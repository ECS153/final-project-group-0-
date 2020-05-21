using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace dotnetapi.Entities
{
    public class User
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Credential> Credentials { get; set; }
        
    }

    public class UserContext : DbContext
    {
        protected readonly IConfiguration _Configuration;
        public UserContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<User> Users { get; set; }
    }
}