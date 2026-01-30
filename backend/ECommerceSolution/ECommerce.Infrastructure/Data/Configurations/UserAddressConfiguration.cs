using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ContactName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ContactPhone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.AddressLine)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Province)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.District)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Ward)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(e => e.IsDefault)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(e => e.User)
                 .WithMany(u => u.UserAddresses)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
