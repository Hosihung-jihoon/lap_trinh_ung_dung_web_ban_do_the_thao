using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.SnapshotProductName)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(e => e.SnapshotSku)
                .HasMaxLength(50);
            
            builder.Property(e => e.SnapshotThumbnail)
                .HasMaxLength(255);

            builder.Property(e => e.UnitPrice)
                .HasPrecision(18, 2);

            builder.Property(e => e.TotalPrice)
                .HasPrecision(18, 2);
            
            builder.Property(e => e.DiscountApplied)
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(e => e.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ProductVariant)
                .WithMany(v => v.OrderDetails)
                .HasForeignKey(e => e.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
