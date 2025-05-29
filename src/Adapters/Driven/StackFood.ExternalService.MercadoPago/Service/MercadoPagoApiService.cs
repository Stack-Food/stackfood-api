using MercadoPago.Client.Payment;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Domain.Enums;

namespace StackFood.ExternalService.MercadoPago.Service
{
    public class MercadoPagoApiService : IMercadoPagoApiService
    {
        public async Task<(long? paymentExternalId, string qrCodeUrl)> GeneratePaymentAsync(
            PaymentType type,
            StackFood.Domain.Entities.Order order,
            StackFood.Domain.Entities.Customer custumer)
        {
            var paymentMethodId = type switch
            {
                PaymentType.Pix => "pix",
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Tipo de pagamento não suportado.")
            };

            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = order.TotalPrice,
                Description = "Descrição da compra",
                PaymentMethodId = paymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email = "stackFood@fiap.com",
                    FirstName = custumer.Name
                }
            };

            var client = new PaymentClient();
            var payment = await client.CreateAsync(paymentRequest);

            if (payment is null)
            {
                return (null, null);
            }

            return (payment.Id, payment.PointOfInteraction.TransactionData.QrCode);
        }
    }
}

