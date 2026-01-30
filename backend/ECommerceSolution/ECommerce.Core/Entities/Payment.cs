using System;

namespace ECommerce.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? PaidAt { get; set; }
        public string? ResponseData { get; set; }
        public string? BankCode { get; set; }
        public string? ErrorCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Order Order { get; set; } = null!;
    }
}