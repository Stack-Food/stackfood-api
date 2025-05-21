using StackFood.API.Requests.Orders;
using StackFood.Application.UseCases.Orders.Create.Inputs;

namespace StackFood.API.Mappers.Orders.CreateOrder
{
    public static class CreateOrderInputMapper
    {
        public static CreateOrderInput Map(CreateOrderRequest request)
        {
            return new CreateOrderInput
            {
                CustomerId = request.CustomerId,
                Products = request.Products?.Select(p => new CreateOrderProductInput
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList() ?? new List<CreateOrderProductInput>()
            };
        }
    }
}
