using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.DiscountType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.DiscountValue)
                .HasPrecision(18, 2);

             builder.Property(e => e.IsActive)
                .HasDefaultValue(true);
            
            builder.Property(e => e.Priority)
                .HasDefaultValue(0);

             builder.Property(e => e.CanStack)
                .HasDefaultValue(false);

            builder.Property(e => e.Description)
                .HasMaxLength(1000);
            
            builder.Property(e => e.MinOrderValue)
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            builder.Property(e => e.MaxDiscount)
                .HasPrecision(18, 2);
            
            builder.Property(e => e.UsedCount)
                .HasDefaultValue(0);
            
            builder.Property(e => e.Thumbnail)
                .HasMaxLength(255);

            // Relationships
            builder.HasMany(e => e.Conditions)
                .WithOne(c => c.Promotion)
                .HasForeignKey(c => c.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
