using System;

namespace ECommerce.Core.Entities
{
    public class ProductPromotion : BaseEntity
    {
        public int ProductId { get; set; }
        public int PromotionId { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual Promotion Promotion { get; set; } = null!;
    }
}