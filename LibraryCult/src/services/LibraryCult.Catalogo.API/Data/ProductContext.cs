using LibraryCult.Catalogo.API.Models;
using LibraryCult.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryCult.Catalogo.API.Data
{
    public class ProductContext : DbContext, IUnityOfWork
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }


        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
