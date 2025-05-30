using StackFood.Domain.Entities;
using StackFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackFood.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task<List<Order>> GetAllAsync(OrderStatus? status);
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetPendingPaymentOrdersAsync();
        Task SaveAsync();
        Task AddPaymentAsync(Payment payment);
    }
}
