using ECommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok("Added to cart");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(int cartItemId, int quantity)
        {
            await _cartService.UpdateCartItemAsync(cartItemId, quantity);
            return Ok("Cart updated");
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveCart(int cartItemId)
        {
            await _cartService.RemoveCartItemAsync(cartItemId);
            return Ok("Removed from cart");
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok("Cart cleared");
        }

        [HttpGet("total/{userId}")]
        public async Task<IActionResult> GetTotal(int userId)
        {
            var total = await _cartService.GetCartTotalAsync(userId);
            return Ok(total);
        }
    }
}