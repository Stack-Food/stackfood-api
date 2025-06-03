using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.GetById;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Products
{
    public class GetByIdProductUseCaseTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly GetByIdProductUseCase _getByIdProductUseCaseMock;

        public GetByIdProductUseCaseTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _getByIdProductUseCaseMock = new GetByIdProductUseCase(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetProductByFilterAsync_ShouldReturnProduct_WhenExists()
        {
            var product = new Product("Product 1", "Description", 10.0m, "http://image.com", ProductCategory.Sandwich);
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(It.IsAny<Guid?>())).ReturnsAsync(product);

            var result = await _getByIdProductUseCaseMock.GetProductByIdAsync(It.IsAny<Guid?>());

            result.Should().NotBeNull();
            result.Name.Should().Be("Product 1");
        }
    }
}
