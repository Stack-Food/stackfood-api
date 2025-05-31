using MercadoPago.Client.Payment;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.ExternalService.MercadoPago.Service
{
    public class MercadoPagoApiService : IMercadoPagoApiService
    {
        public async Task<(long? paymentExternalId, string qrCodeUrl)> GeneratePaymentAsync(
            PaymentType type,
            Order order,
            Customer customer)
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
                    FirstName = customer.Name
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

        public async Task<PaymentStatus> GetPaymentStatusAsync(Order order)
        {
            // TODO: Mock para testes
            if (order.Customer.Name.Contains("PAGO"))
                return PaymentStatus.Paid;

            if (order.Customer.Name.Contains("CANCELADO"))
                return PaymentStatus.Cancelled;
            
            return PaymentStatus.Pending;

            if (order.Payment == null)
                throw new Exception("Order.Payment is null. Cannot retrieve ExternalPaymentId.");

            var client = new PaymentClient();
            var payment = await client.GetAsync(order.Payment.PaymentExternalId);

            if (payment is null)
                throw new Exception($"Erro ao consultar Mercado Pago");

            return payment.Status switch
            {
                "approved" => PaymentStatus.Paid,
                "pending" => PaymentStatus.Pending,
                "rejected" => PaymentStatus.Cancelled,
                "cancelled" => PaymentStatus.Cancelled,
                _ => PaymentStatus.Pending
            };
        }
    }
}

