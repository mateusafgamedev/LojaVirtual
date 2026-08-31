using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // Define your DbSets here, for example:
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel { Id = 1, Nome = "Eletrônicos" },
                new CategoriaModel { Id = 2, Nome = "Causados" },
                new CategoriaModel { Id = 3, Nome = "Roupas" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
