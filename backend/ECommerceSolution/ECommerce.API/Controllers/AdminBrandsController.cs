using ECommerce.Application.DTOs.Brand;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/admin/brands")]
    // [Authorize(Roles = "Admin")] // Uncomment if you want to secure these endpoints
    public class AdminBrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public AdminBrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BrandDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _brandService.GetAllBrandsAdminAsync();
            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto dto)
        {
            var result = await _brandService.CreateBrandAsync(dto);
            // Since we don't have GetById endpoint right now, we can just return Created with the entity
            return result.Success ? StatusCode(201, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] UpdateBrandDto dto)
        {
            var result = await _brandService.UpdateBrandAsync(id, dto);
            if (!result.Success && result.Message == "Brand not found.") return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            if (!result.Success && result.Message == "Brand not found.") return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
