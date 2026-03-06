using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Product;

namespace ECommerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDto>> GetProductsAsync(ProductQueryParameters queryParameters);
        Task<ProductDto?> GetProductBySlugAsync(string slug);
        Task<IEnumerable<ProductVariantDto>> GetProductVariantsAsync(int productId);
        Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync(int count = 10);
        Task<IEnumerable<ProductDto>> GetNewArrivalsAsync(int count = 10);
        Task<IEnumerable<ProductDto>> GetBestSellersAsync(int count = 10);
        Task<IEnumerable<ProductDto>> GetRelatedProductsAsync(int productId, int count = 5);
    }
}
