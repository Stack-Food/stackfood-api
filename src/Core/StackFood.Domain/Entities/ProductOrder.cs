namespace StackFood.Domain.Entities
{
    public class ProductOrder
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Order Order { get; private set; } = null!;
        public Product Product { get; private set; } = null!;

        protected ProductOrder() { }

        public ProductOrder(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
