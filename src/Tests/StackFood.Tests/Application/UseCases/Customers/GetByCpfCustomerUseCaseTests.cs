using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Customers.GetByCpf;
using StackFood.Domain.Entities;

namespace StackFood.UnitTests.Application.UseCases.Customers
{
    public class GetByCpfCustomerUseCaseTests
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly GetByCpfCustomerUseCase _getByCpfCustomerUseCase;

        public GetByCpfCustomerUseCaseTests()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _getByCpfCustomerUseCase = new GetByCpfCustomerUseCase(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetByCpfAsync_ShouldReturnCustomer_WhenFound()
        {
            // Arrange
            var cpf = "12345678901";
            var expectedCustomer = new Customer("João", "joao@email.com", cpf);

            _repositoryMock.Setup(r => r.GetByCpfAsync(cpf))
                .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _getByCpfCustomerUseCase.GetByCpfAsync(cpf);

            // Assert
            result.Should().BeEquivalentTo(expectedCustomer);
            _repositoryMock.Verify(r => r.GetByCpfAsync(cpf), Times.Once);
        }

        [Fact]
        public async Task GetByCpfAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var cpf = "00000000000";
            _repositoryMock.Setup(r => r.GetByCpfAsync(cpf))
                .ReturnsAsync((Customer?)null);

            // Act
            var result = await _getByCpfCustomerUseCase.GetByCpfAsync(cpf);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(r => r.GetByCpfAsync(cpf), Times.Once);
        }
    }
}
