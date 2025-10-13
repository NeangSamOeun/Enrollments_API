using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Data
{
    public class ProductDataDbContext : DbContext
    {
        public ProductDataDbContext(DbContextOptions<ProductDataDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(buildAction =>
            {
                buildAction.ToTable("Proudcts");
                buildAction.Property(p => p.Id).HasColumnType("varchar").HasMaxLength(50);
                buildAction.Property(p => p.Code).HasColumnType("varchar").HasMaxLength(100).IsRequired();
                buildAction.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(100).IsRequired();
                buildAction.Property(p => p.Price).HasColumnType("decimal(18,2)");
                buildAction.Property(p => p.Stock).HasColumnType("int");

                buildAction.Property(p => p.Id); // primary key
                buildAction.ToTable(t =>
                {
                    t.HasCheckConstraint("CHK_CODE_NOT_EMPTY", $"{nameof(Product.Code)} <> ''");
                    t.HasCheckConstraint("CHK_PRICE_POSITIVE", $"{nameof(Product.Price)} >= 0.0");
                    t.HasCheckConstraint("CHK_STOCK_POSITIVE", $"{nameof(Product.Stock)} >= 0");
                });
                buildAction.HasIndex(p => p.Code).IsUnique();
                buildAction.HasIndex(p => p.Name).IsUnique().HasFilter("Name <> ''");
            });
        }
    }
}
