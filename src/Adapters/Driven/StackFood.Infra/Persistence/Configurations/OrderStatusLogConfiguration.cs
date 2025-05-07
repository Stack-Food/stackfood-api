using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Configurations
{
    public class OrderStatusLogConfiguration : IEntityTypeConfiguration<OrderStatusLog>
    {
        public void Configure(EntityTypeBuilder<OrderStatusLog> builder)
        {
            builder.ToTable("order_status_logs");

            builder.HasKey(log => log.Id);

            builder.Property(log => log.OldStatus)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(log => log.NewStatus)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(log => log.ChangedAt)
                   .IsRequired();
        }
    }
}
