namespace ECommerce.Application.DTOs.Cart
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string? ProductThumbnail { get; set; }
        public int ProductVariantId { get; set; }
        public string VariantSku { get; set; } = string.Empty;
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}