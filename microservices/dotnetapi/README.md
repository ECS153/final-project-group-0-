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

### Creating a New Credential

### Requesting to Use a Credential

## Design Decisions
### Credential Encryption
exactly why we decided to have the Pi send the private key rather than store the credentials / send them to the server
