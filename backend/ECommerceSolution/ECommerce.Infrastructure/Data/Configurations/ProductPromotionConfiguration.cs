using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {
            builder.ToTable("ProductPromotions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.DiscountedPrice)
                .HasPrecision(18, 2);
            
            builder.Property(e => e.StartDate);
            builder.Property(e => e.EndDate);

            // Relationships
            builder.HasOne(e => e.Product)
                .WithMany(p => p.ProductPromotions)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Promotion)
                .WithMany(p => p.ProductPromotions)
                .HasForeignKey(e => e.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
