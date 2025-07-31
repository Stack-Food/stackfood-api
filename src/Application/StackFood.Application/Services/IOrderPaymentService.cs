using StackFood.Domain.Entities;
using StackFood.Domain.Enums; // Add this line if PaymentStatus is in this namespace

namespace StackFood.Application.Services
{
    public interface IOrderPaymentService
    {
        Task<List<Order>> GetPendingPaymentOrdersAsync();
        Task UpdateOrderPaymentStatusAsync(Order order, PaymentStatus status);
    }
}