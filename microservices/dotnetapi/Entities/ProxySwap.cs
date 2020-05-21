using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System;
namespace dotnetapi.Entities
{
    public class ProxySwap
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Ip { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string RandToken { get; set; }
        [Required]
        public string Credential {get; set; }
    }

    public class ProxySwapContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ProxySwapContext(IConfiguration configuration) {
        Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder opts) {
                        Console.Write("hello");
        opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<ProxySwap> ProxySwaps { get; set; }
    }
}

