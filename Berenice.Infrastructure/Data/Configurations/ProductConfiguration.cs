using Berenice.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Berenice.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Brand)
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Category)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            builder.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
