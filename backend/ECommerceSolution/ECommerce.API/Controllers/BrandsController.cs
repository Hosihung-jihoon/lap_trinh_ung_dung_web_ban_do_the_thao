using ECommerce.Application.DTOs.Brand;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService; // Service của Member 2

        public BrandsController(IBrandService brandService, IProductService productService)
        {
            _brandService = brandService;
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BrandDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _brandService.GetActiveBrandsAsync();
            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBrandBySlug(string slug)
        {
            var result = await _brandService.GetBrandBySlugAsync(slug);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("{slug}/products")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByBrandSlug(string slug)
        {
            // Sử dụng service của Member 2
            var result = await _productService.GetProductsByBrandSlugAsync(slug);
            return result.Success ? Ok(result) : StatusCode(500, result);
        }
    }
}
