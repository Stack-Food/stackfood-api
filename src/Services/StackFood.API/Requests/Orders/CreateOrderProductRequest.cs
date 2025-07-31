namespace StackFood.API.Requests.Orders
{
    public class CreateOrderProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}