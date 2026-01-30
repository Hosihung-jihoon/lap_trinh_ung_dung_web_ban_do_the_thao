namespace ECommerce.Core.Entities
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
        public bool IsMain { get; set; } = false;
        public string? AltText { get; set; }

        // Navigation property
        public virtual Product Product { get; set; } = null!;
    }
}