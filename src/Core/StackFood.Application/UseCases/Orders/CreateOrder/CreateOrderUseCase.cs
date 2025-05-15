using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.CreateOrder.Inputs;
using StackFood.Application.UseCases.Orders.CreateOrder.Mappers;
using StackFood.Application.UseCases.Orders.CreateOrder.Outputs;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Orders.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderOutput> CreateOrderAsync(CreateOrderInput input)
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

            return CreateOrderOutputMapper.Map(order);
        }
    }
}
