using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : BaseController
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid userId, decimal totalAmount)
        {
            var result = await _orderService.CreateOrder(userId, totalAmount);
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyOrders(Guid userId)
        {
            var orders = await _orderService.GetMyOrders(userId);
            return Ok(orders);
        }
    }
}