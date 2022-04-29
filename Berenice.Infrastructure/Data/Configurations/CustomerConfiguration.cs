using Berenice.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Berenice.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    { 
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.Property(e => e.State)
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false);
        }
    }
}
