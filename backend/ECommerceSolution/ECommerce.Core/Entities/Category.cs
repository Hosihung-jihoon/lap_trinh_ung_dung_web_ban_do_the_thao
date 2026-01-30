using System.Collections.Generic;

namespace ECommerce.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int SortOrder { get; set; } = 0;
        public int ProductCount { get; set; } = 0;

        // Navigation properties
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}