using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(255);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(p => p.ImageUrl).HasMaxLength(255);
            builder.Property(p => p.Category)
                   .HasConversion<string>()
                   .IsRequired();
        }
    }
}
