using StackFood.Domain.Enums;

namespace StackFood.API.Requests.Orders
{
    public class ChangeStatusRequest
    {
        public OrderStatus Status { get; set; }
    }
}