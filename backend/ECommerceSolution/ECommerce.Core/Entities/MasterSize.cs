using System.Collections.Generic;

namespace ECommerce.Core.Entities
{
    public class MasterSize : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public int SortOrder { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}