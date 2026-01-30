using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(1000);
            
            builder.Property(e => e.IsRead)
                .HasDefaultValue(false);

            builder.Property(e => e.Type)
                .HasMaxLength(50);
            
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            
            builder.Property(e => e.Link)
                .HasMaxLength(500);

            // Relationships
             builder.HasOne(e => e.User)
                 .WithMany(u => u.Notifications)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
