using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Guid userId, decimal totalAmount)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TotalAmount = totalAmount,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(order);

            var history = new OrderStatusHistory
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.OrderStatusHistories.Add(history);

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetMyOrders(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ToListAsync();
        }

        public async Task UpdateStatus(Guid orderId, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
                throw new Exception("Order not found");

            order.Status = status;

            var history = new OrderStatusHistory
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                Status = status,
                CreatedAt = DateTime.Now
            };

            _context.OrderStatusHistories.Add(history);

            await _context.SaveChangesAsync();
        }
    }
}