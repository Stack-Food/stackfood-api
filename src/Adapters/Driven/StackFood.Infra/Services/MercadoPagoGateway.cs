using Microsoft.Extensions.Configuration;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StackFood.Infra.Services
{
    public class MercadoPagoGateway : IExternalPaymentGateway
    {
        private readonly string _accessToken;
        private readonly HttpClient _httpClient;

        public MercadoPagoGateway(IConfiguration configuration)
        {
            _accessToken = configuration["MercadoPago:AccessToken"] ?? throw new Exception("MercadoPago AccessToken not configured.");
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        public async Task<PaymentStatus> GetPaymentStatusAsync(Order order)
        {
            // Supondo que você armazene o PaymentId do Mercado Pago na entidade Payment
            if (order.Payment == null)
                throw new Exception("Order.Payment is null. Cannot retrieve ExternalPaymentId.");

            var paymentId = order.Payment.ExternalPaymentId; // ajuste conforme seu modelo

            var url = $"https://api.mercadopago.com/v1/payments/{paymentId}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao consultar Mercado Pago: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var status = doc.RootElement.GetProperty("status").GetString();

            // Mapeie o status do Mercado Pago para o seu enum PaymentStatus
            return status switch
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