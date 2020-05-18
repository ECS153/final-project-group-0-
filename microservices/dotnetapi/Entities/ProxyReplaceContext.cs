using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dotnetapi.Entities
{
  public class ProxyReplaceContext : DbContext
  {
    protected readonly IConfiguration Configuration;
    public ProxyReplaceContext(IConfiguration configuration) {
      Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder opts) {
      opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }
    public DbSet<ProxyReplace> ProxyReplaces { get; set; }
  }
}