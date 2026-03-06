using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid orderId, decimal amount)
        {
            var payment = await _paymentService.CreatePayment(orderId, amount);
            return Ok(payment);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var payments = await _paymentService.GetPaymentsByOrder(orderId);
            return Ok(payments);
        }
    }
}