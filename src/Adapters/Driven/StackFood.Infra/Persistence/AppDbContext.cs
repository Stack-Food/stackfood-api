using Microsoft.EntityFrameworkCore;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
