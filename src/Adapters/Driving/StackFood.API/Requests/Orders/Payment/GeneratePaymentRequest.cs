using StackFood.Domain.Enums;

namespace StackFood.API.Requests.Orders.Payment
{
    public class GeneratePaymentRequest
    {
        public PaymentType Type { get; set; }
    }
}
