using ECommerce.Application.Services.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Application.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CartItem>> GetCartAsync(int userId)
        {
            return await _unitOfWork.CartItems
                .FindAsync(x => x.UserId == userId);
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var item = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            await _unitOfWork.CartItems.AddAsync(item);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var item = await _unitOfWork.CartItems.GetByIdAsync(cartItemId);

            if (item == null) return;

            item.Quantity = quantity;

            _unitOfWork.CartItems.Update(item);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var item = await _unitOfWork.CartItems.GetByIdAsync(cartItemId);

            if (item == null) return;

            _unitOfWork.CartItems.Delete(item);
            await _unitOfWork.SaveAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var items = await _unitOfWork.CartItems.FindAsync(x => x.UserId == userId);

            foreach (var item in items)
            {
                _unitOfWork.CartItems.Delete(item);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            var items = await _unitOfWork.CartItems.FindAsync(x => x.UserId == userId);

            decimal total = 0;

            foreach (var item in items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                total += product.Price * item.Quantity;
            }

            return total;
        }
    }
}