namespace StackFood.Application.UseCases.Orders.Create.Inputs
{
    public class CreateOrderProductInput
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
