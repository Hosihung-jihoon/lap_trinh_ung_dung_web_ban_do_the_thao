using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.IsUsed)
                .HasDefaultValue(false);
            
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

             builder.Property(e => e.Description)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(e => e.User)
                 .WithMany(u => u.Coupons)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Promotion)
                .WithMany(p => p.Coupons)
                .HasForeignKey(e => e.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
