using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace dotnetapi.Entities
{
    public class RequestSwap
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username {get; set;}
        [Required]
        public int FieldId { get; set; }
        [Required]
        public string Ip { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string RandToken { get; set; }
    }
    public class RequestSwapContext : DbContext
    {
        protected readonly IConfiguration _Configuration;
        public RequestSwapContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<RequestSwap> RequestSwaps { get; set; }
    }
}