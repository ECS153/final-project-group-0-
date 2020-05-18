using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dotnetapi.Entities
{
    public class UserContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public UserContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<User> Users { get; set; }
    }
}