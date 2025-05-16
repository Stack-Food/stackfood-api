using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Orders.Create.Inputs;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Orders.Create
{
    public class GetOrderOrderUseCase : ICreateOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;

        public GetOrderOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderOutput> CreateOrderAsync(CreateOrderInput input)
        {
            var productsOrders = new List<ProductOrder>();

            foreach (var product in input.Products)
            {
                // TODO: buscar cada item no banco de dados e pegar preço atual

                var productOrder = new ProductOrder(product.ProductId, product.Quantity, 123);
                productsOrders.Add(productOrder);
            }

            var order = new Order(input.CustomerId, productsOrders);

            _orderRepository.CreateAsync(order);

            return OrderOutputMapper.Map(order);
        }
    }
}
