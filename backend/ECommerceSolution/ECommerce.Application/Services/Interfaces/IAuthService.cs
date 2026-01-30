using ECommerce.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace ECommerce.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto googleLoginDto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task LogoutAsync(int userId);
    }
}