using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Orders.Create.Inputs;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.Create
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;
        public readonly ICustomerRepository _customerRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderOutput> CreateOrderAsync(CreateOrderInput input)
        {
            var customer = await _customerRepository.GetByIdAsync(input.CustomerId);
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer not found");
                

            }

            var order = new Order(input.CustomerId);

            foreach (var product in input.Products)
            {
                // TODO: buscar cada item no banco de dados e pegar preço atual

                



                var productOrder = new ProductOrder(
                    product.ProductId,
                    "Teste",
                    "Descriçao",
                    "imageUrl",
                    ProductCategory.Sandwich,
                    product.Quantity,
                    123);
                order.AddProduct(productOrder);
            }

            

            await _orderRepository.CreateAsync(order);

            return OrderOutputMapper.Map(order);
        }
    }
}
