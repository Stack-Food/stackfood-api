namespace StackFood.Application.UseCases.Orders.CreateOrder.Inputs
{
    public class CreateOrderInput
    {
        public Guid CustomerId { get; set; }
        public IEnumerable<CreateOrderProductInput> Products { get; set; }
    }
}
