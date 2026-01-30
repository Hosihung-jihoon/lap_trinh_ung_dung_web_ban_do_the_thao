using System;

namespace ECommerce.Core.Entities
{
    public class OrderStatusHistory : BaseEntity
    {
        public int OrderId { get; set; }
        public int? PreviousStatus { get; set; }
        public int NewStatus { get; set; }
        public string? Note { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int? ChangedByUserId { get; set; }

        // Navigation property
        public virtual Order Order { get; set; } = null!;
    }
}