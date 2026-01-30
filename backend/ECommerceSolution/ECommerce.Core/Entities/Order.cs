using System;
using System.Collections.Generic;

namespace ECommerce.Core.Entities
{
    public class Order : BaseEntity
    {
        public string OrderCode { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string ShippingName { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingPhone { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public string? CouponCode { get; set; }
        public decimal ShippingFee { get; set; } = 0;
        public decimal FinalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Unpaid";
        public int Status { get; set; } = 0; // 0=Pending, 1=Confirmed, 2=Shipping, 3=Delivered, 4=Cancelled
        public DateTime? UpdatedAt { get; set; }
        public string? Notes { get; set; }
        public string? TrackingNumber { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<OrderStatusHistory> StatusHistories { get; set; } = new List<OrderStatusHistory>();
        public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}