namespace ECommerce.Application.DTOs.Promotion
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public decimal MinOrderValue { get; set; }
        public decimal? MaxDiscount { get; set; }
    }

    public class CreatePromotionDto
    {
        public string Name { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public decimal MinOrderValue { get; set; }
        public decimal? MaxDiscount { get; set; }
    }

    public class UpdatePromotionDto
    {
        public string Name { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public decimal MinOrderValue { get; set; }
        public decimal? MaxDiscount { get; set; }
    }
}