using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class PromotionConditionConfiguration : IEntityTypeConfiguration<PromotionCondition>
    {
        public void Configure(EntityTypeBuilder<PromotionCondition> builder)
        {
            builder.ToTable("PromotionConditions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Field)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Operator)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.LogicalOperator)
                .HasMaxLength(10);
            
            // Relationships
             builder.HasOne(e => e.Promotion)
                .WithMany(p => p.Conditions)
                .HasForeignKey(e => e.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
