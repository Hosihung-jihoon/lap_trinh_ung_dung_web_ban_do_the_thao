namespace ECommerce.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int? BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? Thumbnail { get; set; }
        public bool IsActive { get; set; }
        public int Views { get; set; }
        public int SoldCount { get; set; }
        public decimal AverageRating { get; set; }
        public List<ProductImageDto> Images { get; set; } = new();
        public List<ProductVariantDto> Variants { get; set; } = new();
    }

    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Thumbnail { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }

    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Thumbnail { get; set; }
        public bool IsActive { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }

    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public bool IsMain { get; set; }
        public string? AltText { get; set; }
    }

    public class CreateProductImageDto
    {
        public string ImageUrl { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public bool IsMain { get; set; }
        public string? AltText { get; set; }
    }

    public class ProductVariantDto
    {
        public int Id { get; set; }
        public int? ColorId { get; set; }
        public string? ColorName { get; set; }
        public int? SizeId { get; set; }
        public string? SizeName { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceModifier { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDefault { get; set; }
        public decimal? Weight { get; set; }
    }

    public class CreateProductVariantDto
    {
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceModifier { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDefault { get; set; }
        public decimal? Weight { get; set; }
    }

    public class UpdateProductVariantDto
    {
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceModifier { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDefault { get; set; }
        public decimal? Weight { get; set; }
    }
}