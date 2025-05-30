using Microsoft.EntityFrameworkCore;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Infra.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<List<Order>> GetAllAsync(OrderStatus? status)
        {
            var query = _context.Orders
                       .Include(o => o.Products)
                       .Include(c => c.Customer)
                       .Include(p => p.Payment)
                       .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            query = query.OrderBy(o => o.CreatedAt);

            return await query.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .Include(c => c.Customer)
                .Include(p => p.Payment)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetPendingPaymentOrdersAsync()
        {
            return await _context.Orders
                .Include(c => c.Customer)
                .Include(p => p.Payment)
                .Where(o => o.Payment != null)
                .Where(o => o.Payment.Status == PaymentStatus.Pending)
                .ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
