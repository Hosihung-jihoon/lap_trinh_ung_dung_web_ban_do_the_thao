using System.Collections.Generic;

namespace ECommerce.Core.Entities
{
    public class ProductVariant : BaseEntity
    {
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public decimal PriceModifier { get; set; } = 0;
        public string? ImageUrl { get; set; }
        public bool IsDefault { get; set; } = false;
        public decimal? Weight { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual MasterColor? Color { get; set; }
        public virtual MasterSize? Size { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}