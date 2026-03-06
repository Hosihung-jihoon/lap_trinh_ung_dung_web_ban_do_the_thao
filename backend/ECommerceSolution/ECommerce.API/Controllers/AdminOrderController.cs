using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/admin/orders")]
    public class AdminOrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public AdminOrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, string status)
        {
            await _orderService.UpdateStatus(id, status);
            return Ok();
        }
    }
}