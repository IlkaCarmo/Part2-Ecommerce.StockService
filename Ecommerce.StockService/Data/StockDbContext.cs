using Ecommerce.StockService.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.StockService.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Name).IsRequired().HasMaxLength(100);
                entity.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
                entity.Property(o => o.Stock).IsRequired();
            });
        }
    }
}
