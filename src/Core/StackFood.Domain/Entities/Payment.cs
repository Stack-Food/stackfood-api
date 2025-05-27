using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public string QrCodeUrl { get; private set; }
        public PaymentStatus Status { get; private set; }

        public void UpdateStatus(PaymentStatus status)
        {
            Status = status;
        }

        public DateTime PaymentDate { get; private set; }
        public PaymentType Type { get; set; }
        public Order Order { get; private set; } = null!;

        public Payment()
        {

        }
        public Payment(string qrCode)
        {
            Id = Guid.NewGuid();
            Status = PaymentStatus.Pending;
            PaymentDate = DateTime.UtcNow;
            QrCodeUrl = qrCode;
        }


        public void MarkAsPaid() => Status = PaymentStatus.Paid;

        public string ExternalPaymentId { get; set; }
    }
}
