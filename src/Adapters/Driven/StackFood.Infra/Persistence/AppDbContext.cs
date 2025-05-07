using Microsoft.EntityFrameworkCore;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<ProductOrder> ProductsOrders => Set<ProductOrder>();
        public DbSet<OrderStatusLog> OrderStatusLogs => Set<OrderStatusLog>();
        public DbSet<Payment> Payments => Set<Payment>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
