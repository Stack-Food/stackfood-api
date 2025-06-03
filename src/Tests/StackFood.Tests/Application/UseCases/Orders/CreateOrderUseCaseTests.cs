using FluentAssertions;
using Moq;
using StackFood.Application.Common;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Create;
using StackFood.Application.UseCases.Orders.Create.Inputs;
using StackFood.Domain.Entities;

namespace StackFood.UnitTests.Application.UseCases.Orders
{
    public class CreateOrderUseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly CreateOrderUseCase _useCase;

        public CreateOrderUseCaseTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _productRepoMock = new Mock<IProductRepository>();
            _customerRepoMock = new Mock<ICustomerRepository>();

            _useCase = new CreateOrderUseCase(
                _orderRepoMock.Object,
                _productRepoMock.Object,
                _customerRepoMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrderSuccessfully()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var input = new CreateOrderInput
            {
                CustomerId = customerId,
                Products =
            [
                new CreateOrderProductInput { ProductId = productId, Quantity = 2 }
            ]
            };

            var customer = new Customer("John", "john@email.com", "44444444412");
            var product = new Product("hamburguer", "Product 1", 10.0m, "img.jpg", Domain.Enums.ProductCategory.Sandwich);

            _customerRepoMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);
            _productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _orderRepoMock.Setup(r => r.CreateAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _orderRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.CreateOrderAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();

            var orderOutput = result.Value;
            orderOutput.Should().NotBeNull();
            orderOutput.Customer?.Id.Should().Be(customerId);
            orderOutput.Products.Should().HaveCount(1);
            orderOutput.Products.First().ProductId.Should().Be(productId);

            _orderRepoMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Once);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowArgumentNullException_WhenCustomerNotFound()
        {
            // Arrange
            var input = new CreateOrderInput
            {
                CustomerId = Guid.NewGuid(),
                Products = new List<CreateOrderProductInput>()
            };

            _customerRepoMock.Setup(r => r.GetByIdAsync(input.CustomerId.Value)).ReturnsAsync((Customer?)null);

            // Act
            var result = await _useCase.CreateOrderAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Cliente não encontrado.");
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowArgumentNullException_WhenProductNotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var input = new CreateOrderInput
            {
                CustomerId = customerId,
                Products =
            [
                new CreateOrderProductInput { ProductId = productId, Quantity = 1 }
            ]
            };

            var customer = new Customer("John", "john@email.com", "44444444412");
            var product = new Product("hamburguer", "Product 1", 10.0m, "img.jpg", Domain.Enums.ProductCategory.Sandwich);

            _customerRepoMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);
            _productRepoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product?)null);

            // Act
            // Act
            var result = await _useCase.CreateOrderAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Produto não encontrado.");
        }
    }
}
