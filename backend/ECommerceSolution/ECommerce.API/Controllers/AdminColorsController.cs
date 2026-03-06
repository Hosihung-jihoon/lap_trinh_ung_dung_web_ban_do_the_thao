using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.MasterData;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/admin/colors")]
    // [Authorize(Roles = "Admin")] // Uncomment if you want to secure these endpoints
    public class AdminColorsController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;

        public AdminColorsController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MasterColorDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllColors()
        {
            var result = await _masterDataService.GetAllColorsAsync();
            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetColorById(int id)
        {
            var result = await _masterDataService.GetColorByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateColor([FromBody] CreateMasterColorDto dto)
        {
            var result = await _masterDataService.CreateColorAsync(dto);
            return result.Success ? CreatedAtAction(nameof(GetColorById), new { id = result.Data!.Id }, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<MasterColorDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] UpdateMasterColorDto dto)
        {
            var result = await _masterDataService.UpdateColorAsync(id, dto);
            if (!result.Success && result.Message == "Color not found") return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var result = await _masterDataService.DeleteColorAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
