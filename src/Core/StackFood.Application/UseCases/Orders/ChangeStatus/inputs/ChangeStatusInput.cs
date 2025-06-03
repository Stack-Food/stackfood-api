using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.ChangeStatus.inputs
{
    public class ChangeStatusInput
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}