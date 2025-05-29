using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StackFood.API.Controllers;
using StackFood.API.Requests.Customers;
using StackFood.Application.UseCases.Customers.Create;
using StackFood.Application.UseCases.Customers.GetByCpf;
using StackFood.Domain.Entities;

namespace StackFood.UnitTests.Api.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICreateCustomerUseCase> _createCustomerUseCase;
        private readonly Mock<IGetByCpfCustomerUseCase> _getByCpfCustomerUseCase;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _createCustomerUseCase = new Mock<ICreateCustomerUseCase>();
            _getByCpfCustomerUseCase = new Mock<IGetByCpfCustomerUseCase>();
            _controller = new CustomerController(_createCustomerUseCase.Object, _getByCpfCustomerUseCase.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_CustomerCreated()
        {
            // Arrange
            var request = new CreateCustomerRequest("João", "joao@email.com", "12345678901");

            // Act
            var result = await _controller.Create(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var customer = okResult.Value.Should().BeOfType<Customer>().Subject;

            customer.Name.Should().Be("João");
            customer.Email.Should().Be("joao@email.com");
            customer.Cpf.Should().Be("12345678901");

            _createCustomerUseCase.Verify(x => x.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetByCpf_ShouldReturnOk_IfFoundCustomer()
        {
            // Arrange
            var cpf = "12345678901";
            var request = new GetCustomerByCpfRequest(cpf);
            var expectedCustomer = new Customer("Maria", "maria@email.com", cpf);

            _getByCpfCustomerUseCase.Setup(x => x.GetByCpfAsync(request.Cpf)).ReturnsAsync(expectedCustomer);

            // Act
            var result = await _controller.GetByCpf(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var customer = okResult.Value.Should().BeOfType<Customer>().Subject;

            customer.Cpf.Should().Be(cpf);
            customer.Name.Should().Be("Maria");

            _getByCpfCustomerUseCase.Verify(x => x.GetByCpfAsync(cpf), Times.Once);
        }

        [Fact]
        public async Task GetByCpf_ShouldReturnNotFound_IfNotFoundCustomer()
        {
            // Arrange
            var request = new GetCustomerByCpfRequest("0000000");
            _getByCpfCustomerUseCase.Setup(x => x.GetByCpfAsync(request.Cpf)).ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.GetByCpf(request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _getByCpfCustomerUseCase.Verify(x => x.GetByCpfAsync(request.Cpf), Times.Once);
        }
    }
}
