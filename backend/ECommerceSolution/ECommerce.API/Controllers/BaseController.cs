using ECommerce.Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        protected string GetCurrentUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }

        protected string GetCurrentUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }

        protected IActionResult Success<T>(T data, string message = "Success")
        {
            return Ok(ApiResponse<T>.SuccessResponse(data, message));
        }

        protected IActionResult Error<T>(string message, List<string>? errors = null)
        {
            return BadRequest(ApiResponse<T>.ErrorResponse(message, errors));
        }
    }
}