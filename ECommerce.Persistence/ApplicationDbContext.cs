using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;

namespace ECommerce.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite key for OrderItem (since it connects Order + Product)
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            // Define relationships explicitly
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // ✅ Seed some initial products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Pen", Description = "Blue Pen", Price = 1.5, Stock = 100 },
                new Product { Id = 2, Name = "Notebook", Description = "A4 Notebook", Price = 3.0, Stock = 50 },
                new Product { Id = 3, Name = "Eraser", Description = "White Eraser", Price = 0.75, Stock = 200 }
            );
        }
    }
}
