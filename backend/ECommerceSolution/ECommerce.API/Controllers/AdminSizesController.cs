using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.MasterData;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/admin/sizes")]
    // [Authorize(Roles = "Admin")] // Uncomment if you want to secure these endpoints
    public class AdminSizesController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;

        public AdminSizesController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MasterSizeDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSizes()
        {
            var result = await _masterDataService.GetAllSizesAsync();
            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSizeById(int id)
        {
            var result = await _masterDataService.GetSizeByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSize([FromBody] CreateMasterSizeDto dto)
        {
            var result = await _masterDataService.CreateSizeAsync(dto);
            return result.Success ? CreatedAtAction(nameof(GetSizeById), new { id = result.Data!.Id }, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<MasterSizeDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSize(int id, [FromBody] UpdateMasterSizeDto dto)
        {
            var result = await _masterDataService.UpdateSizeAsync(id, dto);
            if (!result.Success && result.Message == "Size not found") return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var result = await _masterDataService.DeleteSizeAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
