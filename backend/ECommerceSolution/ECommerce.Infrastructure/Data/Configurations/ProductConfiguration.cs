using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasIndex(p => p.Slug)
                .IsUnique();

            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Property(p => p.AverageRating)
                .HasPrecision(3, 2);
                
            // Relationships
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}