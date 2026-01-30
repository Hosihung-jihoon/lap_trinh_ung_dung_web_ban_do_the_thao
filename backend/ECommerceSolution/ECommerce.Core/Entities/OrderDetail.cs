namespace ECommerce.Core.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public string SnapshotProductName { get; set; } = string.Empty;
        public string SnapshotSku { get; set; } = string.Empty;
        public string? SnapshotThumbnail { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountApplied { get; set; } = 0;

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual ProductVariant ProductVariant { get; set; } = null!;
    }
}