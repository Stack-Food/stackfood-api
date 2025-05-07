using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Configurations
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.Property(c => c.Cpf).IsRequired().HasMaxLength(14);
            builder.HasIndex(c => c.Cpf).IsUnique();
        }
    }
}