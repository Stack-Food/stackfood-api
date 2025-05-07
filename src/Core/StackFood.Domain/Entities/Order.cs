using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string QrCodeUrl { get; private set; }

        public Customer? Customer { get; private set; }
        public IEnumerable<ProductOrder> ProductsOrders { get; private set; }
        public IEnumerable<OrderStatusLog> StatusLogs { get; private set; }
        public Payment? Payment { get; private set; }

        protected Order() { }

        public Order(Guid? customerId, decimal totalPrice, string qrCodeUrl)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            TotalPrice = totalPrice;
            QrCodeUrl = qrCodeUrl;
            Status = OrderStatus.Received;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(OrderStatus newStatus) => Status = newStatus;
    }
}
