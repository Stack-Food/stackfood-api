namespace StackFood.API.Requests.Orders
{
    public class CreateOrderRequest
    {
        public Guid? CustomerId { get; set; }
        public IEnumerable<CreateOrderProductRequest> Products { get; set; }
    }
}
