using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.Slug)
                .IsUnique();

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);
            
            // Relationships
             builder.HasMany(e => e.Articles)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
