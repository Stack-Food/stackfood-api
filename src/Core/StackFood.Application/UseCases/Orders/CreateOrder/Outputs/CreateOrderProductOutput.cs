using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.CreateOrder.Outputs
{
    public class CreateOrderProductOutput
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public ProductCategory Category { get; set; }
    }
}
