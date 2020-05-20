using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Entities
{
    public class Credential
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string 

    }

    public class CredentialContext : DbContext
    {
        protected readonly IConfiguration _Configuration;
        public CredentialContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Credential> Credentials { get; set; }
    }
}