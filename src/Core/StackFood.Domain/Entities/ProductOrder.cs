using StackFood.Domain.Enums;

namespace StackFood.Domain.Entities
{
    public class ProductOrder
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public ProductCategory Category { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Order Order { get; private set; } = null!;

        protected ProductOrder() { }

        public ProductOrder(
            Guid productId,
            string name,
            string description,
            string imageUrl,
            ProductCategory category,
            int quantity,
            decimal unitPrice
            )
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Category = category;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
