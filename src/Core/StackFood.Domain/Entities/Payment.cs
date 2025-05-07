using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime PaymentDate { get; private set; }

        public Order Order { get; private set; } = null!;

        protected Payment() { }

        public Payment(Guid orderId)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Status = PaymentStatus.Pending;
            PaymentDate = DateTime.UtcNow;
        }

        public void MarkAsPaid() => Status = PaymentStatus.Paid;
    }
}
