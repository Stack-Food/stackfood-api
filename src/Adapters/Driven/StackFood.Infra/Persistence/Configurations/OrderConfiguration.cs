using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.QrCodeUrl).HasMaxLength(255);

            builder.HasOne(o => o.Customer)
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.Products)
                   .WithOne(i => i.Order)
                   .HasForeignKey("OrderId");


            builder.HasOne(o => o.Payment)
                   .WithOne(p => p.Order)
                   .HasForeignKey<Payment>("OrderId");
        }
    }
}
