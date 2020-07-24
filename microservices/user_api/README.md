# SECRET API
## Introduction
This microservice is in charge of syncronizing and managing all of the other microservices. With the exception of handling user logins, it follows the RESTful api design pattern. We decided to use Dotnet Core because we felt that it had robust authentication modules that we could integrate fairly easily. We only allow https connections to the api.

## Credential Encryption
As previously mentioned, to encrypt the credentials on the server, we decided to create a public/private key pair where the private key is stored only on the pi. While it's never ideal to transport a private key over any connection, we reasoned that since we only allow connections over https, that this would be sufficient to protect the private key from being seen by attackers.

## (Relevant) Code Explained
Looking back at our codebase for the API, there's a lot to explain, and unfortunately I don't think we can explain everything in just this readme alone. Part of the problem is how convoluted dotnet appears to people unfamiliar with it, and part of it is definitely our inexperience with coding big projects. But another factor was also our ambitions. We kept moving the goalpost further and further by adding support for users, administrators, encrypting credentials, and even logging suspiscious activity. So we're only going to outline the most important parts of code that play a role in the SECRET API.

### User Registration
One of our goals was to be able to encrypt all of the user's credentials on our server. To accomplish this, we decided to use RSA public key encryption. Upon registration, a user will receive a private key that can decrypt his credentials on the server. The server will delete the private key, but keep the public key so that it can encrypt any new credentials added without requiring unnecessary transport of the private key. In the design document for the api we discuss our reasoning for such an encryption more in depth
```c#
File: /services/UserService.cs
public String Create(User user, string password, string role)
{
    if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");

    if (_context.Users.Any(x => x.Username == user.Username))
        throw new AppException("Username \"" + user.Username + "\" is already taken");

    byte[] passwordHash, passwordSalt;
    CreatePasswordHash(password, out passwordHash, out passwordSalt);
    user.Role = role;
    user.PasswordHash = passwordHash;
    user.PasswordSalt = passwordSalt;

    // Generating public and private key for credential encryption
    var cryptoServiceProvider = new RSACryptoServiceProvider(2048);
    var privateKey = cryptoServiceProvider.ExportParameters(true); 
    var publicKey = cryptoServiceProvider.ExportParameters(false); 

    string publicKeyString = GetKeyString(publicKey);
    string privateKeyString = GetKeyString(privateKey);

    user.PublicCredKey = publicKeyString;

    _context.Users.Add(user);
    _context.SaveChanges();

    return privateKeyString;
}
```  
This method gets called when the user visits `apiurl/users/new` (via post request). We obviously hash the password and compare against the hash instead of actually storing the password. Then the public/private key pair is created, and the public key is stored on our database, and the private key is sent back to the user. 

### User Login
```c#
File: /Controllers/UserController.cs
public IActionResult Authenticate([FromBody]UserAuthenticateModel model)
{
    var user = _userService.Authenticate(model);
    if (user == null)
        return BadRequest(new { message = "Username or password is incorrect" });

    // Issue Auth Token
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_AppSettings.Secret);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);

    return Ok(new{Token = tokenString});
}
```
This function handles authentication for both the Raspberry Pi and the Chrome extension. When either of them visit `apiurl/user/authenticate` (via post request), it creates and returns a JWT token that is valid for 24 hours, though the expiration date can easily be changed. Our primary concern with handling authentication, was the fact that both the pi and the extension would login with the same credentials. But what if the user's computer that he is running the extension on becomes compromised? How could we prevent malware from stealing the user's password and username and login to our api and "emulate" the pi to bypass the 2 factor authentication? Because only the pi has the private key, even if someone were to attempt this attack, they would only be able to get meaningless garbage since they still would not have the private key, and the encrypted credentials would be not decrypted properly.

### Creating a New Credential
```c#
file: controllers/CredentialController.cs
private static string Encrypt(string textToEncrypt, string publicKeyString)
{
    var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

    using (var rsa = new RSACryptoServiceProvider(2048))
    {
        try {               
            rsa.FromXmlString(publicKeyString.ToString());
            var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
            var base64Encrypted = Convert.ToBase64String(encryptedData);
            return base64Encrypted;
        } finally {
            rsa.PersistKeyInCsp = false;
        }
    }
}
```
Here, we are encrypting the new credential using the public key that was generated when the user created the account. 

### Requesting to Use a Credential
#### 1. User sends a request to our api via the browser extension
```c#
file: controllers/swapController.cs
[HttpPost("new")]
public IActionResult New([FromBody] SubmitRequestModel model)
{
    int userId = int.Parse(User.Identity.Name);
    var ReqSwap = _mapper.Map<RequestSwap>(model);

    ReqSwap.UserId = userId; 
    ReqSwap.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

    _swapService.Enqueue(ReqSwap);

    return Ok();
}
```
Here, we grab the userId from our authentication service that makes sure the extension has a valid JWT token. In 
our `SubmitRequestModel` we already have:
 - `Type`: lets us know if the credentials is a password, username, email, or credit card
 - `AuthId`: A random int from 0-9999 that the user can see on both the browser extension and the Pi GUI, which should let the user know that the request he made was the same one he is authenticating
 - `Domain`: The domain of the website, so we know what credentials would be allowed to be used on the site
 - `RandToken`: The random token that gets generated by the browser extension that we would later swap with the true credentials
 
Then we create a RequestSwap Object that contains the userId as well as the UserIp so that we can later ensure that the proxy only swap credentials that came from the same IP as the user's initial request.

#### 2.Raspberry Pi prompts the user to authenticate the request via our GUI
This part is fairly simple, and the code doesn't really show much. Basically, the Pi makes a http get request on `apiurl/swaps` and if the return isn't empty then it contains the top of the user's queue'd requests, since the user can make several credential requests at once.

#### 3. The Pi then grabs all credentials that can be used with the domain that was on the Request Swap
Again, this part is fairly straightforward. The pi makes an http get request on `apiurl/credentials`, with query parameters:
 - `Type`: the type of credential it is (password, email, username, or credit card)
 - `Domain`: The domain the credential request was from

#### 4. Rasperry Pi submits the request
```c#
public IActionResult Submit([FromBody]SubmitSwapModel model)
{
    int userId = int.Parse(User.Identity.Name);

    // Grab the top request Swap as well as the credential the user wants to use
    RequestSwap reqSwap = _swapService.Front(userId);
    if (reqSwap == null) {
        return BadRequest(new {Title = "User does not have any pending request Swaps"});
    }

    Credential cred = new Credential();
    cred.Id = model.CredentialId;
    cred.UserId = userId;
    cred.Domain = reqSwap.Domain;
    cred = _credService.Read(cred)[0];
    if (cred == null) {
        return BadRequest(new {Title = "User is not allowed to use this credential on this domain"});
    }

    try {
        String valueHash = Decrypt(cred.ValueHash, model.PrivateKey);
        _swapService.Swap(userId, valueHash);
        return Ok();
    }
    catch (AppException e) {
        return BadRequest(new { Title = e.Message });

    // If private key is not correct, this exception will trigger
    } catch(FormatException e) {
        return BadRequest(new { Title = e.Message });
    } catch(Exception e) {
        return BadRequest(new {Title = e.Message});
    }
}
```
The `SubmitSwapModel` object contains:
- `SwapId`: This is the id of the Request, that the Pi got from step 2
- `CredentialId`: This is the id of the credential that the user selected that the Pi knows from step 3 
- `PrivateKey`: This is the private key that the API will use to decrypt the credentials 
The API will find the credential with it's ID, decrypt it's hashed value using the private key it got from the Pi, and then finally put it in the ProxySwap Database table, which is the only table that our proxy has. 


