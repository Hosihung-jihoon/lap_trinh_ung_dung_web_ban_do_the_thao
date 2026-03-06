using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(Guid orderId, decimal amount)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                Amount = amount,
                Status = "Paid",
                Method = "COD",
                CreatedAt = DateTime.Now
            };

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<List<Payment>> GetPaymentsByOrder(Guid orderId)
        {
            return await _context.Payments
                .Where(p => p.OrderId == orderId)
                .ToListAsync();
        }
    }
}