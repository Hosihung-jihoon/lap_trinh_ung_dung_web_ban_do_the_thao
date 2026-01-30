using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.TransactionId)
                .HasMaxLength(100);

            builder.HasIndex(e => e.TransactionId);

            builder.Property(e => e.Amount)
                .HasPrecision(18, 2);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ResponseData)
                 .HasMaxLength(4000); // Reasonable limit for generic text

            builder.Property(e => e.BankCode)
                .HasMaxLength(50);
            
            builder.Property(e => e.ErrorCode)
                .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(e => e.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
