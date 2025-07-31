using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.Base.Outputs
{
    public class OrderPaymentOutput
    {
        public Guid Id { get; set; }
        public long PaymentExternalId { get; set; }
        public string QrCodeUrl { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentType Type { get; set; }
    }
}