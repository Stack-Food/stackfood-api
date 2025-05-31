using FluentAssertions;
using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.GetAll;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Products
{
    public class GetAllProductUseCaseTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly GetAllProductUseCase _getAllProductUseCaseMock;

        public GetAllProductUseCaseTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _getAllProductUseCaseMock = new GetAllProductUseCase(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new("Baicon burger", "hamb de baicon com queijo salada", 10.0m, "http://image1.com", ProductCategory.SideDish),
                new("queijo burger", "hamb de queijo com salada 2", 20.0m, "http://image2.com", ProductCategory.SideDish)
            };
            _mockProductRepository.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            var result = await _getAllProductUseCaseMock.GetAllProductsAsync();

            result.Count.Should().Be(2);
            result[0].Name.Should().Be("Baicon burger");
        }
    }
}
