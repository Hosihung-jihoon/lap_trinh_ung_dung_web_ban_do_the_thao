using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Sku)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasIndex(e => e.Sku)
                .IsUnique();

            builder.Property(e => e.Quantity)
                .HasDefaultValue(0);

            builder.Property(e => e.PriceModifier)
                .HasPrecision(18, 2);
            
            builder.Property(e => e.Weight)
                .HasPrecision(10, 2);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(255);

             builder.Property(e => e.IsDefault)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(e => e.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Color)
                .WithMany(c => c.ProductVariants)
                .HasForeignKey(e => e.ColorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Size)
                .WithMany(s => s.ProductVariants)
                .HasForeignKey(e => e.SizeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
