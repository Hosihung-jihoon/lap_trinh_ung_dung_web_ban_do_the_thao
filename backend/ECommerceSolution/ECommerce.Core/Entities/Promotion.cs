using System;
using System.Collections.Generic;

namespace ECommerce.Core.Entities
{
    public class Promotion : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty; // Percentage, FixedAmount
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int Priority { get; set; } = 0;
        public bool CanStack { get; set; } = false;
        public string? Description { get; set; }
        public decimal MinOrderValue { get; set; } = 0;
        public decimal? MaxDiscount { get; set; }
        public int? UsageLimit { get; set; }
        public int UsedCount { get; set; } = 0;
        public string? Thumbnail { get; set; }

        // Navigation properties
        public virtual ICollection<PromotionCondition> Conditions { get; set; } = new List<PromotionCondition>();
        public virtual ICollection<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();
        public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }
}