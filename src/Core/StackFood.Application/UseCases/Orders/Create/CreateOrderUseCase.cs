using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Orders.Create.Inputs;
using StackFood.Domain.Entities;
namespace StackFood.Application.UseCases.Orders.Create
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IProductRepository _productRepository;
        public readonly ICustomerRepository _customerRepository;

        public CreateOrderUseCase(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
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

            foreach (var inputProduct in input.Products)
            {
                var product = await _productRepository.GetByIdAsync(inputProduct.ProductId);
                if (product is null)
                {
                    throw new ArgumentNullException(nameof(product), "Product not found");
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

            return OrderOutputMapper.Map(order);
        }
    }
}
