using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public long PaymentExternalId { get; private set; }
        public string QrCodeUrl { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public PaymentType Type { get; set; }  
        public Order Order { get; private set; } = null!;

        private Payment()
        {
            QrCodeUrl = string.Empty;
        }

        public Payment(PaymentType paymentType, long paymentExternalId, string qrCode)
        {
            Id = Guid.NewGuid();
            PaymentExternalId = paymentExternalId;
            Status = PaymentStatus.Pending;
            Type = paymentType;
            PaymentDate = DateTime.UtcNow;
            QrCodeUrl = qrCode;
        }

        public void MarkAsPaid() => Status = PaymentStatus.Paid;
    }
}
