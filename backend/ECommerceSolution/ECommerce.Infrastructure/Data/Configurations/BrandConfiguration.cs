using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.Slug)
                .IsUnique();

            builder.Property(e => e.LogoUrl)
                .HasMaxLength(255);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.IsActive)
                 .HasDefaultValue(true);

            // Relationships
             builder.HasMany(e => e.Products)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
