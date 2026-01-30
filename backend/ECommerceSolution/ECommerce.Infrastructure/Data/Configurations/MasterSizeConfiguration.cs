using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class MasterSizeConfiguration : IEntityTypeConfiguration<MasterSize>
    {
        public void Configure(EntityTypeBuilder<MasterSize> builder)
        {
            builder.ToTable("MasterSizes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Type)
                .HasMaxLength(50);
            
            builder.Property(e => e.SortOrder)
                .HasDefaultValue(0);
        }
    }
}
