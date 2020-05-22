using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace dotnetapi.Entities
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration _Configuration;
        public DatabaseContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ProxySwap> ProxySwaps { get; set; }
    }
}