using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Payments.Check;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Orders
{
    public class CheckPaymentUseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IMercadoPagoApiService> _mercadoPagoMock;
        private readonly CheckPaymentUseCase _useCase;

        public CheckPaymentUseCaseTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _mercadoPagoMock = new Mock<IMercadoPagoApiService>();
            _useCase = new CheckPaymentUseCase(_orderRepoMock.Object, _mercadoPagoMock.Object);
        }

        [Fact]
        public async Task CheckPaymentAsync_WhenStatusIsPaid_ShouldMarkOrderAsPaidAndSave()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.GeneratePayment(PaymentType.Pix, 12345, Guid.NewGuid().ToString());

            var orders = new List<Order> { order };

            _orderRepoMock.Setup(r => r.GetPendingPaymentOrdersAsync()).ReturnsAsync(orders);
            _mercadoPagoMock.Setup(m => m.GetPaymentStatusAsync(order)).ReturnsAsync(PaymentStatus.Paid);
            _orderRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _useCase.CheckPaymentAsync();

            // Assert
            order.Payment?.Status.Should().Be(PaymentStatus.Paid);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CheckPaymentAsync_WhenStatusIsCancelled_ShouldMarkOrderAsCancelledAndSave()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.GeneratePayment(PaymentType.Pix, 12345, Guid.NewGuid().ToString());

            var orders = new List<Order> { order };

            _orderRepoMock.Setup(r => r.GetPendingPaymentOrdersAsync()).ReturnsAsync(orders);
            _mercadoPagoMock.Setup(m => m.GetPaymentStatusAsync(order)).ReturnsAsync(PaymentStatus.Cancelled);
            _orderRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _useCase.CheckPaymentAsync();

            // Assert
            order.Payment?.Status.Should().Be(PaymentStatus.Cancelled);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CheckPaymentAsync_WhenStatusIsPending_ShouldNotCallPaidOrCancelled()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.GeneratePayment(PaymentType.Pix, 12345, Guid.NewGuid().ToString());

            var orders = new List<Order> { order };

            _orderRepoMock.Setup(r => r.GetPendingPaymentOrdersAsync()).ReturnsAsync(orders);
            _mercadoPagoMock.Setup(m => m.GetPaymentStatusAsync(order)).ReturnsAsync(PaymentStatus.Pending);
            _orderRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _useCase.CheckPaymentAsync();

            // Assert
            order.Payment?.Status.Should().Be(PaymentStatus.Pending);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CheckPaymentAsync_WithMultipleOrders_ShouldCallSaveForEach()
        {
            // Arrange
            var order1 = new Order(Guid.NewGuid());
            order1.GeneratePayment(PaymentType.Pix, 12345, Guid.NewGuid().ToString());

            var order2 = new Order(Guid.NewGuid());
            order2.GeneratePayment(PaymentType.Pix, 12345, Guid.NewGuid().ToString());

            var orders = new List<Order> { order1, order2 };

            _orderRepoMock.Setup(r => r.GetPendingPaymentOrdersAsync()).ReturnsAsync(orders);
            _mercadoPagoMock.Setup(m => m.GetPaymentStatusAsync(order1)).ReturnsAsync(PaymentStatus.Paid);
            _mercadoPagoMock.Setup(m => m.GetPaymentStatusAsync(order2)).ReturnsAsync(PaymentStatus.Cancelled);
            _orderRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _useCase.CheckPaymentAsync();

            // Assert
            order1.Payment?.Status.Should().Be(PaymentStatus.Paid);
            order2.Payment?.Status.Should().Be(PaymentStatus.Cancelled);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task CheckPaymentAsync_WhenNoPendingOrders_ShouldNotCallSave()
        {
            // Arrange
            _orderRepoMock.Setup(r => r.GetPendingPaymentOrdersAsync()).ReturnsAsync(new List<Order>());

            // Act
            await _useCase.CheckPaymentAsync();

            // Assert
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Never);
            _mercadoPagoMock.Verify(m => m.GetPaymentStatusAsync(It.IsAny<Order>()), Times.Never);
        }
    }
}
