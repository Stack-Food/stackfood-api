using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.Delete;

namespace StackFood.UnitTests.Application.UseCases.Products
{
    public class DeleteProductUseCaseTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly DeleteProductUseCase _deleteProductUseCaseMock;

        public DeleteProductUseCaseTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _deleteProductUseCaseMock = new DeleteProductUseCase(_mockProductRepository.Object);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallRepositoryMethod()
        {
            Guid productId = Guid.NewGuid();

            await _deleteProductUseCaseMock.DeleteProductAsync(productId);

            _mockProductRepository.Verify(repo => repo.DeleteProductByIdAsync(productId), Times.Once);
        }
    }
}
