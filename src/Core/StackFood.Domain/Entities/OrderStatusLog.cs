using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class OrderStatusLog
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public OrderStatus OldStatus { get; private set; }
        public OrderStatus NewStatus { get; private set; }
        public DateTime ChangedAt { get; private set; }

        public Order Order { get; private set; } = null!;

        protected OrderStatusLog() { }

        public OrderStatusLog(Guid orderId, OrderStatus oldStatus, OrderStatus newStatus)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangedAt = DateTime.UtcNow;
        }
    }
}
