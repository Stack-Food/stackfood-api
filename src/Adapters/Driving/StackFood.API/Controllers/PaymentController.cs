using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;

namespace StackFood.API.Controllers
{
    [Route("api/pix")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePixAsync([FromBody] PixPaymentRequestDto request)
        {
            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = request.Amount,
                Description = request.Description,
                PaymentMethodId = "pix",
                Payer = new PaymentPayerRequest
                {
                    Email = request.PayerEmail,
                    FirstName = request.PayerFirstName,
                    LastName = request.PayerLastName
                }
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(paymentRequest);

            return Ok(new
            {
                Id = payment.Id,
                Status = payment.Status,
                QRCode = payment.PointOfInteraction.TransactionData.QrCode,
                QRCodeBase64 = payment.PointOfInteraction.TransactionData.QrCodeBase64,
                ExpirationDate = payment.DateOfExpiration
            });
        }
    }

    public class PixPaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string PayerEmail { get; set; }
        public string PayerFirstName { get; set; }
        public string PayerLastName { get; set; }
    }
}
