using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.MasterData;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/master")]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        [HttpGet("colors")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MasterColorDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyResult>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColors()
        {
            var result = await _masterDataService.GetAllColorsAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpGet("sizes")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MasterSizeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyResult>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSizes()
        {
            var result = await _masterDataService.GetAllSizesAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }
    }
}
