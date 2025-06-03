using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.Payments.Generate.Inputs
{
    public  class GeneratePaymentInput
    {
        public Guid OrderId { get; set; }
        public PaymentType Type { get; set; }
    }
}
