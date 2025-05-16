namespace StackFood.Application.UseCases.Orders.Create.Inputs
{
    public class CreateOrderInput
    {
        public Guid CustomerId { get; set; }
        public IEnumerable<CreateOrderProductInput> Products { get; set; }
    }
}
