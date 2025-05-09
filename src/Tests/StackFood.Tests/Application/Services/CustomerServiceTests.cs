using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Services;
using StackFood.Domain.Entities;

namespace StackFood.Tests.Application.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _service = new CustomerService(_repositoryMock.Object);
        }

        //[Fact]
        //public async Task RegisterAsync_ShouldCallRepositoryAndReturnId()
        //{
        //    // Arrange
        //    var customer = new Customer("John Doe", "john@example.com", "12345678901");

        //    // Act
        //    var result = await _service.RegisterAsync(customer);

        //    // Assert
        //    _repositoryMock.Verify(r => r.RegisterAsync(customer), Times.Once);
        //    Assert.Equal(customer.Id, result);
        //}

        [Fact]
        public async Task GetByCpfAsync_ShouldReturnCustomer_WhenExists()
        {
            // Arrange
            var cpf = "12345678901";
            var expected = new Customer("Jane Doe", "jane@example.com", cpf);

            _repositoryMock
                .Setup(r => r.GetByCpfAsync(cpf))
                .ReturnsAsync(expected);

            // Act
            var result = await _service.GetByCpfAsync(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Cpf, result?.Cpf);
        }

        [Fact]
        public async Task GetByCpfAsync_ShouldReturnNull_WhenCustomerNotFound()
        {
            // Arrange
            var cpf = "00000000000";

            _repositoryMock
                .Setup(r => r.GetByCpfAsync(cpf))
                .ReturnsAsync((Customer?)null);

            // Act
            var result = await _service.GetByCpfAsync(cpf);

            // Assert
            Assert.Null(result);
        }
    }
}
