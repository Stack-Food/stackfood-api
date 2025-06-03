using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentExternalId).IsRequired();
            builder.Property(p => p.QrCodeUrl).IsRequired().HasMaxLength(2000);

            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(p => p.Type)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(p => p.PaymentDate).IsRequired();
        }
    }
}
