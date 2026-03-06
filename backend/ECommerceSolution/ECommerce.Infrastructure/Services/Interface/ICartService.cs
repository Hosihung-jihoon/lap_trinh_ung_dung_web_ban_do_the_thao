using ECommerce.Core.Entities;

namespace ECommerce.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task UpdateCartItemAsync(int cartItemId, int quantity);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(int userId);
        Task<decimal> GetCartTotalAsync(int userId);
    }
}