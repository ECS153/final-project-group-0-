using dotnetapi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapi.Data
{
  public class ProxyReplaceContext : DbContext
  {
    public ProxyReplaceContext(DbContextOptions<ProxyReplaceContext> options) : base(options)
    {
    }

    public DbSet<ProxyReplace> ProxyReplaces { get; set; }
  }
}