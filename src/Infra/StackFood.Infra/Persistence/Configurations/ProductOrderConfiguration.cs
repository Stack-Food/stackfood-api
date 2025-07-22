using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Configurations
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.ToTable("product_orders");

            builder.HasKey(po => po.Id);

            builder.Property(po => po.Name).IsRequired().HasMaxLength(100);
            builder.Property(po => po.Description).HasMaxLength(255);
            builder.Property(po => po.UnitPrice).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(po => po.ImageUrl).HasMaxLength(255);
            builder.Property(po => po.Category)
                   .HasConversion<string>()
                   .IsRequired();
            builder.Property(po => po.Quantity).IsRequired();
            builder.Property(po => po.UnitPrice).HasColumnType("decimal(10,2)").IsRequired();
        }
    }
}