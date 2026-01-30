using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("ProductReviews");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Rating)
                .IsRequired(); // Could add validation 1-5 in entity validation

            builder.Property(e => e.Comment)
                .HasMaxLength(1000);
            
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            
            builder.Property(e => e.IsApproved)
                .HasDefaultValue(false);
            
            builder.Property(e => e.HelpfulVotes)
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(e => e.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.HasOne(e => e.Order)
                .WithMany(o => o.Reviews)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
