using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

using dotnetapi.Entities;
using dotnetapi.Helpers;
using dotnetapi.Models.Users;

namespace dotnetapi.Services
{
    public interface IUserService
    {
        User Authenticate(UserAuthenticateModel model);
        IEnumerable<User> GetAll();
        UserReadModel GetById(int id);
        UserReadModel Create(UserCreateModel model, string Role);
        void Update(UserUpdateModel model);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(DatabaseContext context, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(UserAuthenticateModel model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);   
            if (user == null)
                return null;
            // check if password is correct
            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public UserReadModel GetById(int id)
        {
            return _mapper.Map<UserReadModel>(_context.Users.Find(id));
        }

        public UserReadModel Create(UserCreateModel model, string Role)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username \"" + model.Username + "\" is already taken");

            var user = _mapper.Map<User>(model);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            user.Role = Role;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return _mapper.Map<UserReadModel>(user);
        }

        public void Update(UserUpdateModel model)
        {
            var user = _context.Users.Find(model.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(model.Username) && model.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == model.Username))
                    throw new AppException("Username " + model.Username + " is already taken");

                user.Username = model.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(model.FirstName))
                user.FirstName = model.FirstName;

            if (!string.IsNullOrWhiteSpace(model.LastName))
                user.LastName = model.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}