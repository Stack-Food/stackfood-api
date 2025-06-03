using StackFood.Application.Common;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Orders.Create.Inputs;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Orders.Create
{
    public class CreateOrderUseCase(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository) : ICreateOrderUseCase
    {
        public readonly IOrderRepository _orderRepository = orderRepository;
        public readonly IProductRepository _productRepository = productRepository;
        public readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Result<OrderOutput>> CreateOrderAsync(CreateOrderInput input)
        {
            Customer customer = null;
            if (input.CustomerId != null)
            {
                customer = await _customerRepository.GetByIdAsync(input.CustomerId.Value);
                if (customer is null)
                {
                    return Result<OrderOutput>.Failure("Cliente não encontrado.");
                }
            }

            var order = new Order(input.CustomerId);

            foreach (var inputProduct in input.Products)
            {
                var product = await _productRepository.GetByIdAsync(inputProduct.ProductId);
                if (product is null)
                {
                    return Result<OrderOutput>.Failure("Produto não encontrado.");
                }

                var productOrder = new ProductOrder(
                    inputProduct.ProductId,
                    product.Name,
                    product.Description,
                    product.ImageUrl,
                    product.Category,
                    inputProduct.Quantity,
                    product.Price);

                order.AddProduct(productOrder);
            }

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveAsync();

            var output = OrderOutputMapper.Map(order);

            return Result<OrderOutput>.Success(output);
        }
    }
}
