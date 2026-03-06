using ECommerce.Application.DTOs.Category;
using ECommerce.Application.DTOs.Common;

namespace ECommerce.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<IEnumerable<CategoryDto>>> GetCategoriesHierarchicalAsync();
        Task<ApiResponse<CategoryDto>> GetCategoryBySlugAsync(string slug);
        
        // Admin Methods
        Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAdminAsync();
        Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto);
        Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto dto);
        Task<ApiResponse<bool>> DeleteCategoryAsync(int id);
        Task<ApiResponse<bool>> ReorderCategoriesAsync(List<ReorderCategoryDto> dtos);
    }
}
