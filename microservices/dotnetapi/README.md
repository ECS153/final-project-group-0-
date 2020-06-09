# SECRET API
## Introduction
This microservice is in charge of syncronizing and managing all of the other microservices. With the exception of handling user logins, it follows the RESTful api design pattern. We decided to use Dotnet Core because we felt that it had robust authentication modules that we could integrate fairly easily.

## Structure
It's a bit outside the scope of this README (as well as outside our scope) to properly explain the structure of Dotnet Core, though, so instead we'll just focus on the most critical parts of the code. Anytime a ". . ." is added, it's merely just code that we felt wasn't relevant to the explanation. The most relevant parts of our code will be found in the `services` subfolder. Everything else is mostly semantics related to Dotnet handling http requests.

## HTTPS
We're using https to 

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
This function handles authentication for both the Raspberry Pi and the Chrome extension. When either of them visit `url/user/authenticate`, it creates and returns a JWT token that is valid for 24 hours, though the expiration date can easily be changed. Our primary concern with handling authentication, was the fact that both the pi and the extension would login with the same credentials. But what if the user's computer that he is running the extension on becomes compromised? How could we prevent malware from stealing the user's password and username and login to our api and "emulate" the pi to bypass the 2 factor authentication? Because only the pi has the private key, even if someone were to attempt this attack, they would only be able to get meaningless garbage since they still would not have the private key.

### Creating a New Credential

### Requesting to Use a Credential

## Design Decisions
### Credential Encryption
exactly why we decided to have the Pi send the private key rather than store the credentials / send them to the server
