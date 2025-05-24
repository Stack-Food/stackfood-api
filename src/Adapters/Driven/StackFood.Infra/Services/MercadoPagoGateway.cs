using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Infra.Services
{
    public class MercadoPagoGateway : IExternalPaymentGateway
    {
        public async Task<PaymentStatus> GetPaymentStatusAsync(Order order)
        {

            throw new NotImplementedException();
        }
    }
}