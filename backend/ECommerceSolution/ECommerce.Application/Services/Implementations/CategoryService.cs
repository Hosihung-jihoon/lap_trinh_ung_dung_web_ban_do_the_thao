using AutoMapper;
using ECommerce.Application.DTOs.Category;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Services.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CategoryDto>>> GetCategoriesHierarchicalAsync()
        {
            try
            {
                // Fetch all active categories
                var allCategories = await _unitOfWork.Categories.FindAsync(c => c.IsActive);
                
                // Usually we build the hierarchy in memory or rely on DB tree. 
                // Let's filter top level categories and map them, automapper can handle mapping children if the list of Categories has the children linked, 
                // but since EF Core sets up navigation properties, if we include them or manually build it:
                
                var topLevelCategories = allCategories.Where(c => c.ParentId == null).OrderBy(c => c.SortOrder).ToList();
                
                // Build the tree manually to ensure it's structured even if lazy loading isn't on
                var dtos = _mapper.Map<IEnumerable<CategoryDto>>(topLevelCategories);
                foreach (var dto in dtos)
                {
                    PopulateChildren(dto, allCategories);
                }

                return ApiResponse<IEnumerable<CategoryDto>>.SuccessResponse(dtos);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<CategoryDto>>.ErrorResponse("An error occurred while retrieving categories.");
            }
        }

        private void PopulateChildren(CategoryDto parentDto, IEnumerable<Category> allCategories)
        {
            var children = allCategories.Where(c => c.ParentId == parentDto.Id).OrderBy(c => c.SortOrder).ToList();
            if (children.Any())
            {
                parentDto.Children = _mapper.Map<List<CategoryDto>>(children);
                foreach (var childDto in parentDto.Children)
                {
                    PopulateChildren(childDto, allCategories);
                }
            }
        }

        public async Task<ApiResponse<CategoryDto>> GetCategoryBySlugAsync(string slug)
        {
            try
            {
                var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
                if (category == null || !category.IsActive)
                {
                    return ApiResponse<CategoryDto>.ErrorResponse("Category not found");
                }

                var dto = _mapper.Map<CategoryDto>(category);
                return ApiResponse<CategoryDto>.SuccessResponse(dto);
            }
            catch (Exception)
            {
                return ApiResponse<CategoryDto>.ErrorResponse("An error occurred while retrieving the category.");
            }
        }

        // Admin Methods
        public async Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAdminAsync()
        {
            try
            {
                var allCategories = await _unitOfWork.Categories.GetAllAsync();
                var topLevelCategories = allCategories.Where(c => c.ParentId == null).OrderBy(c => c.SortOrder).ToList();
                
                var dtos = _mapper.Map<IEnumerable<CategoryDto>>(topLevelCategories);
                foreach (var dto in dtos)
                {
                    PopulateChildren(dto, allCategories);
                }

                return ApiResponse<IEnumerable<CategoryDto>>.SuccessResponse(dtos);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<CategoryDto>>.ErrorResponse("An error occurred while retrieving categories.");
            }
        }

        public async Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return ApiResponse<CategoryDto>.ErrorResponse("Category name is required.");

                var category = _mapper.Map<Category>(dto);
                category.Slug = GenerateSlug(dto.Name);

                // Make sure slug is unique
                if (await _unitOfWork.Categories.AnyAsync(c => c.Slug == category.Slug))
                {
                    category.Slug = $"{category.Slug}-{Guid.NewGuid().ToString().Substring(0, 5)}";
                }

                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return ApiResponse<CategoryDto>.SuccessResponse(categoryDto, "Category created successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<CategoryDto>.ErrorResponse("An error occurred while creating the category.");
            }
        }

        public async Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category == null)
                    return ApiResponse<CategoryDto>.ErrorResponse("Category not found.");

                if (string.IsNullOrWhiteSpace(dto.Name))
                    return ApiResponse<CategoryDto>.ErrorResponse("Category name is required.");

                bool nameChanged = category.Name != dto.Name;

                _mapper.Map(dto, category);

                if (nameChanged)
                {
                    category.Slug = GenerateSlug(dto.Name);
                    // Make sure slug is unique
                    if (await _unitOfWork.Categories.AnyAsync(c => c.Slug == category.Slug && c.Id != id))
                    {
                        category.Slug = $"{category.Slug}-{Guid.NewGuid().ToString().Substring(0, 5)}";
                    }
                }

                _unitOfWork.Categories.Update(category);
                await _unitOfWork.SaveChangesAsync();

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return ApiResponse<CategoryDto>.SuccessResponse(categoryDto, "Category updated successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<CategoryDto>.ErrorResponse("An error occurred while updating the category.");
            }
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category == null)
                    return ApiResponse<bool>.ErrorResponse("Category not found.");

                // Check if any products are associated with this category
                bool hasProducts = await _unitOfWork.Products.AnyAsync(p => p.CategoryId == id);
                if (hasProducts)
                    return ApiResponse<bool>.ErrorResponse("Cannot delete category because it has products associated with it.");

                // Check if it has child categories
                bool hasChildren = await _unitOfWork.Categories.AnyAsync(c => c.ParentId == id);
                if (hasChildren)
                    return ApiResponse<bool>.ErrorResponse("Cannot delete category because it has child categories.");

                _unitOfWork.Categories.Remove(category);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.SuccessResponse(true, "Category deleted successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<bool>.ErrorResponse("An error occurred while deleting the category.");
            }
        }

        public async Task<ApiResponse<bool>> ReorderCategoriesAsync(List<ReorderCategoryDto> dtos)
        {
            try
            {
                var ids = dtos.Select(d => d.Id).ToList();
                var categories = await _unitOfWork.Categories.FindAsync(c => ids.Contains(c.Id));

                foreach (var category in categories)
                {
                    var dto = dtos.First(d => d.Id == category.Id);
                    category.SortOrder = dto.SortOrder;
                    _unitOfWork.Categories.Update(category);
                }

                await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.SuccessResponse(true, "Categories reordered successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<bool>.ErrorResponse("An error occurred while reordering categories.");
            }
        }

        private string GenerateSlug(string name)
        {
            // Simple slug generation
            string slug = name.ToLowerInvariant();
            
            // Remove diacritics
            var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(slug);
            slug = System.Text.Encoding.ASCII.GetString(bytes);

            // Replace spaces with hyphens and remove invalid chars
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-").Trim('-');

            return slug;
        }
    }
}
