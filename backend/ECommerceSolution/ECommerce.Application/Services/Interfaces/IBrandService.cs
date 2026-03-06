using ECommerce.Application.DTOs.Brand;
using ECommerce.Application.DTOs.Common;

namespace ECommerce.Application.Services.Interfaces
{
    public interface IBrandService
    {
        Task<ApiResponse<IEnumerable<BrandDto>>> GetActiveBrandsAsync();
        Task<ApiResponse<BrandDto>> GetBrandBySlugAsync(string slug);
        
        // Admin Methods
        Task<ApiResponse<IEnumerable<BrandDto>>> GetAllBrandsAdminAsync();
        Task<ApiResponse<BrandDto>> CreateBrandAsync(CreateBrandDto dto);
        Task<ApiResponse<BrandDto>> UpdateBrandAsync(int id, UpdateBrandDto dto);
        Task<ApiResponse<bool>> DeleteBrandAsync(int id);
    }
}
