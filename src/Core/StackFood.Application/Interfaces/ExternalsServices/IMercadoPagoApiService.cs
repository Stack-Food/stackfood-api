using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.Interfaces.ExternalsServices
{
    public interface IMercadoPagoApiService
    {
        Task<(long? paymentExternalId, string qrCodeUrl)> GeneratePaymentAsync(
            PaymentType type,
            Order order,
            Customer? custumer);

        Task<PaymentStatus> GetPaymentStatusAsync(Order order);
    }
}