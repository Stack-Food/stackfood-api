using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Customers.Create;
using StackFood.Domain.Entities;

namespace StackFood.UnitTests.Application.UseCases.Customers
{
    public class CreateCustomerUseCaseTests
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly CreateCustomerUseCase _createCustomerUseCase;

        public CreateCustomerUseCaseTests()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _createCustomerUseCase = new CreateCustomerUseCase(_repositoryMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldCallRepositoryOnce()
        {
            // Arrange
            var customer = new Customer("Teste", "teste@email.com", "12345678901");

            // Act
            await _createCustomerUseCase.CreateCustomerAsync(customer);

            // Assert
            _repositoryMock.Verify(r => r.CreateAsync(customer), Times.Once);
        }
    }
}
