using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.ChangeStatus;
using StackFood.Application.UseCases.Orders.ChangeStatus.inputs;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Orders
{
    public class ChangeStatusOrderUseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly ChangeStatusOrderUseCase _useCase;

        public ChangeStatusOrderUseCaseTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _useCase = new ChangeStatusOrderUseCase(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task ChangeStatusOrderAsync_WhenOrderNotFound_ShouldDoNothing()
        {
            // Arrange
            var input = new ChangeStatusInput() { OrderId = Guid.NewGuid(), Status = OrderStatus.Ready };
            _orderRepositoryMock.Setup(r => r.GetByIdAsync(input.OrderId))
                .ReturnsAsync((Order?)null);


            // Act
            await _useCase.ChangeStatusOrderAsync(input);

            // Assert
            _orderRepositoryMock.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task ChangeStatusOrderAsync_WhenStatusIsReady_ShouldCallReadyAndSave()
        {
            // Arrange
            var orderMock = new Mock<Order>();
            var input = new ChangeStatusInput() { OrderId = Guid.NewGuid(), Status = OrderStatus.Ready };

            _orderRepositoryMock.Setup(r => r.GetByIdAsync(input.OrderId))
                                .ReturnsAsync(orderMock.Object);

            // Act
            await _useCase.ChangeStatusOrderAsync(input);

            // Assert
            orderMock.Object.Status.Should().Be(OrderStatus.Ready);
            _orderRepositoryMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ChangeStatusOrderAsync_WhenStatusIsFinalized_ShouldCallFinalizedAndSave()
        {
            // Arrange
            var orderMock = new Mock<Order>();
            var input = new ChangeStatusInput() { OrderId = Guid.NewGuid(), Status = OrderStatus.Finalized };

            _orderRepositoryMock.Setup(r => r.GetByIdAsync(input.OrderId))
                                .ReturnsAsync(orderMock.Object);

            // Act
            await _useCase.ChangeStatusOrderAsync(input);

            // Assert
            orderMock.Object.Status.Should().Be(OrderStatus.Finalized);
            _orderRepositoryMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ChangeStatusOrderAsync_WithInvalidStatus_ShouldThrowException()
        {
            // Arrange
            var orderMock = new Mock<Order>();
            var input = new ChangeStatusInput() { OrderId = Guid.NewGuid(), Status = (OrderStatus)999 };

            _orderRepositoryMock.Setup(r => r.GetByIdAsync(input.OrderId))
                                .ReturnsAsync(orderMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _useCase.ChangeStatusOrderAsync(input));
        }
    }
}
