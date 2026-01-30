using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                return Success(result, "Registration successful");
            }
            catch (Exception ex)
            {
                return Error<AuthResponseDto>(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Success(result, "Login successful");
            }
            catch (Exception ex)
            {
                return Error<AuthResponseDto>(ex.Message);
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        {
            try
            {
                var result = await _authService.GoogleLoginAsync(googleLoginDto);
                return Success(result, "Google login successful");
            }
            catch (Exception ex)
            {
                return Error<AuthResponseDto>(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(refreshToken);
                return Success(result, "Token refreshed successfully");
            }
            catch (Exception ex)
            {
                return Error<AuthResponseDto>(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                await _authService.ForgotPasswordAsync(forgotPasswordDto);
                return Success<object>(null, "Password reset email sent");
            }
            catch (Exception ex)
            {
                return Error<object>(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                await _authService.ResetPasswordAsync(resetPasswordDto);
                return Success<object>(null, "Password reset successful");
            }
            catch (Exception ex)
            {
                return Error<object>(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResultLogout()
            {
            try
            {
                var userId = GetCurrentUserId();
                await _authService.LogoutAsync(userId);
                return Success<object>(null, "Logout successful");
            }
            catch (Exception ex)
            {
                return Error<object>(ex.Message);
            }
        }
    }
}