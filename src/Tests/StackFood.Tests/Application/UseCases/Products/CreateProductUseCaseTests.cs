using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.Create;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Products
{
    public class CreateProductUseCaseTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly CreateProductUseCase _productUseCaseMock;

        public CreateProductUseCaseTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productUseCaseMock = new CreateProductUseCase(_mockProductRepository.Object);
        }

        [Fact]
        public async Task RegisterNewProductAsync_ShouldCallRepositoryMethod()
        {
            var product = new Product("New Product", "New Description", 15.0m, "http://newimage.com", ProductCategory.SideDish);

            await _productUseCaseMock.RegisterNewProductAsync(product);

            _mockProductRepository.Verify(repo => repo.RegisterProductAsync(product), Times.Once);
        }
    }
}
