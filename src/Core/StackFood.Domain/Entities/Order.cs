using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Customer? Customer { get; private set; }
        public List<ProductOrder> Products { get; private set; }
        public Payment? Payment { get; private set; }

        public decimal TotalPrice => Products.Sum(x => x.Quantity * x.UnitPrice);

        protected Order() { }

        public Order(Guid? customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            

            Status = OrderStatus.Received;
            CreatedAt = DateTime.UtcNow;
            Products = new List<ProductOrder>();
        }

        public void AddProduct(ProductOrder product)
        {
            Products.Add(product);
        }
        public void GeneratePayment(PaymentType paymentType, string qrCode)
        {
            Payment = new Payment(paymentType, qrCode);
        }

        public void GeneratePayment(string qrCode)
        {
            Payment = new Payment(qrCode);
        }
    }
}
