using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // Define your DbSets here, for example:
        // public DbSet<Product> Products { get; set; }
    }
}
