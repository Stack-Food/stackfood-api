using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.Update;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.UnitTests.Application.UseCases.Products
{
    public class UpdateProductUseCaseTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly IUpdateProductUseCase _updateProductUseCaseMock;

        public UpdateProductUseCaseTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _updateProductUseCaseMock = new UpdateProductUseCase(_mockProductRepository.Object);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldCallRepositoryMethod()
        {
            var product = new Product("Updated Product", "Updated Description", 25.0m, "http://updateimage.com", ProductCategory.SideDish);

            await _updateProductUseCaseMock.UpdateProductAsync(product);

            _mockProductRepository.Verify(repo => repo.UpdateProductAsync(product), Times.Once);
        }
    }
}
