using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder.Property(o => o.DiscountAmount)
                .HasPrecision(18, 2);

            builder.Property(o => o.ShippingFee)
                .HasPrecision(18, 2);

            builder.Property(o => o.FinalAmount)
                .HasPrecision(18, 2);

            // Relationships
            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.StatusHistories)
                .WithOne(h => h.Order)
                .HasForeignKey(h => h.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}