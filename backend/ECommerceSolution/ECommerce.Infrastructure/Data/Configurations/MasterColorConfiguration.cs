using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class MasterColorConfiguration : IEntityTypeConfiguration<MasterColor>
    {
        public void Configure(EntityTypeBuilder<MasterColor> builder)
        {
            builder.ToTable("MasterColors");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.HexCode)
                .IsRequired()
                .HasMaxLength(10);
            
            builder.Property(e => e.ImageUrl)
                .HasMaxLength(255);
            
            builder.Property(e => e.SortOrder)
                .HasDefaultValue(0);
        }
    }
}
