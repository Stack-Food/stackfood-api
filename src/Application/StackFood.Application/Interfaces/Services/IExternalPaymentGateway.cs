using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.Interfaces.Services
{
    public interface IExternalPaymentGateway
    {
        Task<PaymentStatus> GetPaymentStatusAsync(Order order);
    }
}