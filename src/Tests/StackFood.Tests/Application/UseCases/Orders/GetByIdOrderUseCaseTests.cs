using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.GetById;
using StackFood.Domain.Entities;

namespace StackFood.UnitTests.Application.UseCases.Orders
{
    public class GetByIdOrderUseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly GetByIdOrderUseCase _useCase;

        public GetByIdOrderUseCaseTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _useCase = new GetByIdOrderUseCase(_orderRepoMock.Object);
        }

        [Fact]
        public async Task GetByIdOrderAsync_ShouldReturnMappedOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order(orderId);
            _orderRepoMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _useCase.GetByIdOrderAsync(orderId);

            // Assert
            result.Should().NotBeNull();
            _orderRepoMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetByIdOrderAsync_WhenOrderNotFound_ShouldReturnEmpty()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync([]);

            // Act
            var result = await _useCase.GetByIdOrderAsync(orderId);

            // Assert
            result.Id.Should().Be(Guid.Empty);
            _orderRepoMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
        }
    }
}
