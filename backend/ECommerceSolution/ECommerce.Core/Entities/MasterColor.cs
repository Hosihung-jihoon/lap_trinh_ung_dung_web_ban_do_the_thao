namespace ECommerce.Core.Entities
{
    public class MasterColor : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int SortOrder { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
