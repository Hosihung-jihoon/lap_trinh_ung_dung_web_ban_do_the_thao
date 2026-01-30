using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(e => e.Slug)
                .IsUnique();

            builder.Property(e => e.Summary)
                 .HasMaxLength(1000);

            builder.Property(e => e.Thumbnail)
                 .HasMaxLength(255);

             builder.Property(e => e.IsPublished)
                .HasDefaultValue(false);

            builder.Property(e => e.Views)
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(e => e.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
