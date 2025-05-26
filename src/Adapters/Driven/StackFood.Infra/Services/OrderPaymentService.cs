using Microsoft.EntityFrameworkCore;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;
using StackFood.Infra.Persistence;

namespace StackFood.Infra.Services
{
    public class OrderPaymentService : IOrderPaymentService
    {
        private readonly AppDbContext _context;

        public OrderPaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetPendingPaymentOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Payment)
                .Where(o => o.Payment != null && o.Payment.Status == PaymentStatus.Pending)
                .ToListAsync();
        }

        public async Task UpdateOrderPaymentStatusAsync(Order order, PaymentStatus status)
        {
            if (order.Payment != null)
            {
                order.Payment.UpdateStatus(status);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Order payment cannot be null.");
            }
        }
    }
}