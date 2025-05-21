using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.Base.Outputs
{
    public class OrderOutput
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderProductOutput> Products { get; set; }
    }
}