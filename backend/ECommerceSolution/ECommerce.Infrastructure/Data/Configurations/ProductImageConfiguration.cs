using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.SortOrder)
                .HasDefaultValue(0);

            builder.Property(e => e.IsMain)
                .HasDefaultValue(false);

            builder.Property(e => e.AltText)
                .HasMaxLength(200);

            // Relationships
            builder.HasOne(e => e.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
