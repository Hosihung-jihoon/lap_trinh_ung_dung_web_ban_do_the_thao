namespace ECommerce.Application.DTOs.Coupon
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string PromotionName { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? Description { get; set; }
    }
}