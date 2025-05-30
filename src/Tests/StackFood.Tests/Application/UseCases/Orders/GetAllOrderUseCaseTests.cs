using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.GetAll;
using StackFood.Domain.Entities;

namespace StackFood.UnitTests.Application.UseCases.Orders
{
    public class GetAllOrderUseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly GetAllOrderUseCase _useCase;

        public GetAllOrderUseCaseTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _useCase = new GetAllOrderUseCase(_orderRepoMock.Object);
        }

        [Fact]
        public async Task GetAllOrderAsync_ShouldReturnMappedOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new(Guid.NewGuid()),
                new(Guid.NewGuid())
            };

            _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _useCase.GetAllOrderAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(orders);
            result.All(o => o is not null).Should().BeTrue();
            _orderRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllOrderAsync_WhenNoOrders_ShouldReturnEmptyList()
        {
            // Arrange
            _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync([]);

            // Act
            var result = await _useCase.GetAllOrderAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _orderRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
