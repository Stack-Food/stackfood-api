using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Payments.Generate;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Orders
{
    public class GeneratePaymentUseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IMercadoPagoApiService> _mercadoPagoMock;
        private readonly GeneratePaymentUseCase _useCase;

        public GeneratePaymentUseCaseTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _customerRepoMock = new Mock<ICustomerRepository>();
            _mercadoPagoMock = new Mock<IMercadoPagoApiService>();

            _useCase = new GeneratePaymentUseCase(
                _orderRepoMock.Object,
                _customerRepoMock.Object,
                _mercadoPagoMock.Object);
        }

        [Fact]
        public async Task GeneratePaymentAsync_ShouldGeneratePaymentSuccessfully()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var input = new GeneratePaymentInput() { OrderId = orderId, Type = PaymentType.Pix };

            var customer = new Customer("John", "john@email.com", "44455566678");
            var order = new Order(customer.Id);

            typeof(Order)
                .GetProperty(nameof(Order.Customer))!
                .SetValue(order, customer);

            _orderRepoMock.Setup(r => r.GetByIdAsync(orderId))
                          .ReturnsAsync(order);

            _customerRepoMock.Setup(r => r.GetByIdAsync(customer.Id))
                             .ReturnsAsync(customer);

            _mercadoPagoMock.Setup(m => m.GeneratePaymentAsync(input.Type, order, customer))
                            .ReturnsAsync((123456789L, "qr_code_here"));

            _orderRepoMock.Setup(r => r.AddPaymentAsync(It.IsAny<Payment>())).Returns(Task.CompletedTask);
            _orderRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _useCase.GeneratePaymentAsync(input);

            // Assert
            order.Payment.Should().NotBeNull();
            order.Payment!.Type.Should().Be(PaymentType.Pix);
            _orderRepoMock.Verify(r => r.AddPaymentAsync(order.Payment!), Times.Once);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GeneratePaymentAsync_WhenOrderNotFound_ShouldDoNothing()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var input = new GeneratePaymentInput() { OrderId = orderId, Type = PaymentType.Pix };

            _orderRepoMock.Setup(r => r.GetByIdAsync(input.OrderId)).ReturnsAsync((Order?)null);

            // Act
            var act = async () => await _useCase.GeneratePaymentAsync(input);

            // Assert
            await act.Should().NotThrowAsync();
            _orderRepoMock.Verify(r => r.AddPaymentAsync(It.IsAny<Payment>()), Times.Never);
            _orderRepoMock.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task GeneratePaymentAsync_WhenOrderAlreadyHasPayment_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var input = new GeneratePaymentInput() { OrderId = orderId, Type = PaymentType.Pix };
            var order = new Order(Guid.NewGuid());
            order.GeneratePayment(PaymentType.Pix, 123456789L, "qr_code_here");

            _orderRepoMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            Func<Task> act = async () => await _useCase.GeneratePaymentAsync(input);

            // Assert
            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Pagamento já foi gerado para este pedido.");
        }

        [Fact]
        public async Task GeneratePaymentAsync_WhenMercadoPagoFails_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var input = new GeneratePaymentInput() { OrderId = orderId, Type = PaymentType.Pix };

            var customer = new Customer("John", "john@email.com", "44455566678");
            var order = new Order(customer.Id);
            
            typeof(Order)
                .GetProperty(nameof(Order.Customer))!
                .SetValue(order, customer);

            _orderRepoMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _customerRepoMock.Setup(r => r.GetByIdAsync(customer.Id)).ReturnsAsync(customer);
            _mercadoPagoMock.Setup(m => m.GeneratePaymentAsync(input.Type, order, customer))
                            .ReturnsAsync((null, "qr_code_here"));

            // Act
            Func<Task> act = async () => await _useCase.GeneratePaymentAsync(input);

            // Assert
            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Falha ao criar pagamento.");
        }
    }
}
