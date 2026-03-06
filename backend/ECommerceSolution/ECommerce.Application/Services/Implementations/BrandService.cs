using AutoMapper;
using ECommerce.Application.DTOs.Brand;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Services.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Application.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<BrandDto>>> GetActiveBrandsAsync()
        {
            try
            {
                var activeBrands = await _unitOfWork.Brands.FindAsync(b => b.IsActive);
                
                var dtos = _mapper.Map<IEnumerable<BrandDto>>(activeBrands);

                return ApiResponse<IEnumerable<BrandDto>>.SuccessResponse(dtos);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<BrandDto>>.ErrorResponse("An error occurred while retrieving brands.");
            }
        }

        public async Task<ApiResponse<BrandDto>> GetBrandBySlugAsync(string slug)
        {
            try
            {
                var brand = await _unitOfWork.Brands.FirstOrDefaultAsync(b => b.Slug == slug);
                if (brand == null || !brand.IsActive)
                {
                    return ApiResponse<BrandDto>.ErrorResponse("Brand not found");
                }

                var dto = _mapper.Map<BrandDto>(brand);
                return ApiResponse<BrandDto>.SuccessResponse(dto);
            }
            catch (Exception)
            {
                return ApiResponse<BrandDto>.ErrorResponse("An error occurred while retrieving the brand.");
            }
        }

        // Admin Methods
        public async Task<ApiResponse<IEnumerable<BrandDto>>> GetAllBrandsAdminAsync()
        {
            try
            {
                var allBrands = await _unitOfWork.Brands.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<BrandDto>>(allBrands);
                return ApiResponse<IEnumerable<BrandDto>>.SuccessResponse(dtos);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<BrandDto>>.ErrorResponse("An error occurred while retrieving brands.");
            }
        }

        public async Task<ApiResponse<BrandDto>> CreateBrandAsync(CreateBrandDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return ApiResponse<BrandDto>.ErrorResponse("Brand name is required.");

                var brand = _mapper.Map<Brand>(dto);
                brand.Slug = GenerateSlug(dto.Name);

                // Make sure slug is unique
                if (await _unitOfWork.Brands.AnyAsync(b => b.Slug == brand.Slug))
                {
                    brand.Slug = $"{brand.Slug}-{Guid.NewGuid().ToString().Substring(0, 5)}";
                }

                await _unitOfWork.Brands.AddAsync(brand);
                await _unitOfWork.SaveChangesAsync();

                var brandDto = _mapper.Map<BrandDto>(brand);
                return ApiResponse<BrandDto>.SuccessResponse(brandDto, "Brand created successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<BrandDto>.ErrorResponse("An error occurred while creating the brand.");
            }
        }

        public async Task<ApiResponse<BrandDto>> UpdateBrandAsync(int id, UpdateBrandDto dto)
        {
            try
            {
                var brand = await _unitOfWork.Brands.GetByIdAsync(id);
                if (brand == null)
                    return ApiResponse<BrandDto>.ErrorResponse("Brand not found.");

                if (string.IsNullOrWhiteSpace(dto.Name))
                    return ApiResponse<BrandDto>.ErrorResponse("Brand name is required.");

                bool nameChanged = brand.Name != dto.Name;

                _mapper.Map(dto, brand);

                if (nameChanged)
                {
                    brand.Slug = GenerateSlug(dto.Name);
                    // Make sure slug is unique
                    if (await _unitOfWork.Brands.AnyAsync(b => b.Slug == brand.Slug && b.Id != id))
                    {
                        brand.Slug = $"{brand.Slug}-{Guid.NewGuid().ToString().Substring(0, 5)}";
                    }
                }

                _unitOfWork.Brands.Update(brand);
                await _unitOfWork.SaveChangesAsync();

                var brandDto = _mapper.Map<BrandDto>(brand);
                return ApiResponse<BrandDto>.SuccessResponse(brandDto, "Brand updated successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<BrandDto>.ErrorResponse("An error occurred while updating the brand.");
            }
        }

        public async Task<ApiResponse<bool>> DeleteBrandAsync(int id)
        {
            try
            {
                var brand = await _unitOfWork.Brands.GetByIdAsync(id);
                if (brand == null)
                    return ApiResponse<bool>.ErrorResponse("Brand not found.");

                // Check if any products are associated with this brand
                bool hasProducts = await _unitOfWork.Products.AnyAsync(p => p.BrandId == id);
                if (hasProducts)
                    return ApiResponse<bool>.ErrorResponse("Cannot delete brand because it has products associated with it.");

                _unitOfWork.Brands.Remove(brand);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.SuccessResponse(true, "Brand deleted successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<bool>.ErrorResponse("An error occurred while deleting the brand.");
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
