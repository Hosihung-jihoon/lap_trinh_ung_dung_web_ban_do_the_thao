using System;

namespace ECommerce.Core.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int PromotionId { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public int? UsageLimit { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Promotion Promotion { get; set; } = null!;
    }
}