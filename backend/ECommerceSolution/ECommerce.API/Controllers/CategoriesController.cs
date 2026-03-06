using ECommerce.Application.DTOs.Category;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService; // Service của Member 2

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategoriesHierarchicalAsync();
            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            var result = await _categoryService.GetCategoryBySlugAsync(slug);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("{slug}/products")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByCategorySlug(string slug)
        {
            // Sử dụng service của Member 2
            var result = await _productService.GetProductsByCategorySlugAsync(slug);
            return result.Success ? Ok(result) : StatusCode(500, result);
        }
    }
}
