namespace StackFood.Application.UseCases.Orders.CreateOrder.Inputs
{
    public class CreateOrderProductInput
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
