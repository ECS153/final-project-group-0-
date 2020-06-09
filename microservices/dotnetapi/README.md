# SECRET API
## Introduction
This microservice is in charge of syncronizing and managing all of the other microservices. With the exception of handling user logins, it follows the RESTful api design pattern. We decided to use Dotnet Core because we felt that it had robust authentication modules that we could integrate fairly easily.

## Structure
It's a bit outside the scope of this README (as well as outside our scope) to properly explain the structure of Dotnet Core, though, so instead we'll just focus on the most critical parts of the code.

### User Registration

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



### User Login

### Creating a New Credential

### Requesting to Use a Credential
