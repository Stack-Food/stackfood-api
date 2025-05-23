using StackFood.Domain.Entities;
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
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(Guid id);
        Task SaveAsync();
    }
}
