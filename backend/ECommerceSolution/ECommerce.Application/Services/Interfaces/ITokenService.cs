using ECommerce.Core.Entities;

namespace ECommerce.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        int? ValidateToken(string token);
    }
}