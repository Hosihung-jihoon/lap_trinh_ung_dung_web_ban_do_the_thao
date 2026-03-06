using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<PagedResponse<ProductDto>>> GetProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            var pagedProducts = await _productService.GetProductsAsync(queryParameters);
            return Ok(pagedProducts);
        }

        // GET: api/products/{slug}
        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string slug)
        {
            var product = await _productService.GetProductBySlugAsync(slug);

            if (product == null)
            {
                return NotFound(new { message = $"Product with slug '{slug}' not found." });
            }

            return Ok(product);
        }

        // GET: api/products/{id}/variants
        [HttpGet("{id:int}/variants")]
        public async Task<ActionResult<IEnumerable<ProductVariantDto>>> GetProductVariants(int id)
        {
            var variants = await _productService.GetProductVariantsAsync(id);
            return Ok(variants);
        }

        // GET: api/products/featured
        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetFeaturedProducts([FromQuery] int count = 10)
        {
            var products = await _productService.GetFeaturedProductsAsync(count);
            return Ok(products);
        }

        // GET: api/products/new-arrivals
        [HttpGet("new-arrivals")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetNewArrivals([FromQuery] int count = 10)
        {
            var products = await _productService.GetNewArrivalsAsync(count);
            return Ok(products);
        }

        // GET: api/products/best-sellers
        [HttpGet("best-sellers")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetBestSellers([FromQuery] int count = 10)
        {
            var products = await _productService.GetBestSellersAsync(count);
            return Ok(products);
        }

        // GET: api/products/{id}/related
        [HttpGet("{id:int}/related")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetRelatedProducts(int id, [FromQuery] int count = 5)
        {
            var products = await _productService.GetRelatedProductsAsync(id, count);
            return Ok(products);
        }
    }
}
