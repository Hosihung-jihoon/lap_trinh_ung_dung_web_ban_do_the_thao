using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.ToTable("OrderStatusHistories");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Note)
                .HasMaxLength(500);

            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(100);
            
            builder.Property(e => e.Timestamp)
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(e => e.Order)
                .WithMany(o => o.StatusHistories)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
