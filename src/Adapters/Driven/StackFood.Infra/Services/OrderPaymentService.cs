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
                .Where(o => o.Payment.Status == PaymentStatus.Pending)
                .ToListAsync();
        }

        public async Task UpdateOrderPaymentStatusAsync(Order order, PaymentStatus status)
        {
            order.Payment.UpdateStatus(status); // Implemente esse método na entidade Payment
            await _context.SaveChangesAsync();
        }
    }
}