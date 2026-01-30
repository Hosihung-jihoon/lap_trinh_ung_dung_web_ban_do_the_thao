using System;

namespace ECommerce.Core.Entities
{
    public class ProductReview : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        public int HelpfulVotes { get; set; } = 0;

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}