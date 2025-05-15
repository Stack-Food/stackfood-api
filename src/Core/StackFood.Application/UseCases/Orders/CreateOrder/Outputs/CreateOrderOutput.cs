using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.CreateOrder.Outputs
{
    public class CreateOrderOutput
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CreateOrderProductOutput> Products { get; set; }
    }
}