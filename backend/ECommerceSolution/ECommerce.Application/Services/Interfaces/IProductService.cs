using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Product;

namespace ECommerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        // This method will be implemented by Member 2
        Task<ApiResponse<IEnumerable<ProductDto>>> GetProductsByCategorySlugAsync(string categorySlug);
        
        // This method will be implemented by Member 2
        Task<ApiResponse<IEnumerable<ProductDto>>> GetProductsByBrandSlugAsync(string brandSlug);
    }
}
