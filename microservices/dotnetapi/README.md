# SECRET API
## Introduction
This microservice is in charge of syncronizing and managing all of the other microservices. With the exception of handling user logins, it follows the RESTful api design pattern. We decided to use Dotnet Core because we felt that it had robust authentication modules that we could integrate fairly easily.

## Structure
It's a bit outside the scope of this README (as well as outside our scope) to properly explain the structure of Dotnet Core, though, so instead we'll just focus on the most critical parts of the code. Anytime a ". . ." is added, it's merely just code that we felt wasn't relevant to the explanation. The most relevant parts of our code will be found in the `services` subfolder. Everything else is mostly semantics related to Dotnet handling http requests.

## HTTPS
We're using https for all communication to and from the api. While we have seen ways attackers can get around HTTPS encryption, most of them time they would involve changing or adding certificates to a user's computer. 

### User Registration
One of our goals was to be able to encrypt all of the user's credentials on our server. To accomplish this, we decided to use RSA public key encryption. Upon registration, a user will receive a private key that can decrypt his credentials on the server. The server will delete the private key, but keep the public key so that it can encrypt any new credentials added without requiring unnecessary transport of the private key. In the design decisions section, we discuss our encryption more in depth
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
This method gets called when the user visits `url/users/new`. We obviously hash the password and compare against the hash instead of actually storing the password. Then the public/private key pair is created, and the public key is stored on our database, and the private key is sent back to the user. 

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
This function handles authentication for both the Raspberry Pi and the Chrome extension. When either of them visit `url/user/authenticate`, it creates and returns a JWT token that is valid for 24 hours, though the expiration date can easily be changed. Our primary concern with handling authentication, was the fact that both the pi and the extension would login with the same credentials. But what if the user's computer that he is running the extension on becomes compromised? How could we prevent malware from stealing the user's password and username and login to our api and "emulate" the pi to bypass the 2 factor authentication? Because only the pi has the private key, even if someone were to attempt this attack, they would only be able to get meaningless garbage since they still would not have the private key, and the encrypted credentials would be not decrypted properly.

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
 -`Type`: lets us know if the credentials is a password, username, email, or credit card
 - `AuthId`: A random int from 0-9999 that the user can see on both the browser extension and the Pi GUI, which should let the user know that the request he made was the same one he is authenticating
 - `Domain`: The domain of the website, so we know what credentials would be allowed to be used on the site
 - `RandToken`: The random token that gets generated by the browser extension that we would later swap with the true credentials
 
Then we create a RequestSwap Object that contains the userId as well as the UserIp so that we can later ensure that the proxy only swap credentials that came from the same IP as the user's initial request.

#### 2.Raspberry Pi prompts the user to authenticate the request via our GUI, and then grabs all credentials that can be used with the domain that the 

```c#


```

#### 3. Rasperry Pi submits the request



## Design Decisions
### Credential Encryption
exactly why we decided to have the Pi send the private key rather than store the credentials / send them to the server
