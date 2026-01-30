using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Quantity)
                .HasDefaultValue(1);

            builder.Property(e => e.AddedAt)
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(e => e.User)
                 .WithMany(u => u.CartItems)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductVariant)
                .WithMany(v => v.CartItems)
                .HasForeignKey(e => e.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
