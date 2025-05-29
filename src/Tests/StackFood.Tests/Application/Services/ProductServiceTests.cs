using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.Create;
using StackFood.Application.UseCases.Products.Delete;
using StackFood.Application.UseCases.Products.GetAll;
using StackFood.Application.UseCases.Products.GetById;
using StackFood.Application.UseCases.Products.Update;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Tests.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;

        private readonly CreateProductUseCase _productUseCaseMock;
        private readonly IUpdateProductUseCase _updateProductUseCaseMock;
        private readonly GetAllProductUseCase _getAllProductUseCaseMock;
        private readonly GetByIdProductUseCase _getByIdProductUseCaseMock;
        private readonly DeleteProductUseCase _deleteProductUseCaseMock;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();

            _productUseCaseMock = new CreateProductUseCase(_mockProductRepository.Object);
            _updateProductUseCaseMock = new UpdateProductUseCase(_mockProductRepository.Object);
            _getAllProductUseCaseMock = new GetAllProductUseCase(_mockProductRepository.Object);
            _getByIdProductUseCaseMock = new GetByIdProductUseCase(_mockProductRepository.Object);
            _deleteProductUseCaseMock = new DeleteProductUseCase(_mockProductRepository.Object);

        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new Product("Baicon burger", "hamb de baicon com queijo salada", 10.0m, "http://image1.com", ProductCategory.SideDish),
                new Product("queijo burger", "hamb de queijo com salada 2", 20.0m, "http://image2.com", ProductCategory.SideDish)
            };
            _mockProductRepository.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            var result = await _getAllProductUseCaseMock.GetAllProductsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("Baicon burger", result[0].Name);
        }

        [Fact]
        public async Task GetProductByFilterAsync_ShouldReturnProduct_WhenExists()
        {
            var product = new Product("Product 1", "Description", 10.0m, "http://image.com", ProductCategory.Sandwich);
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(It.IsAny<Guid?>())).ReturnsAsync(product);

            var result = await _getByIdProductUseCaseMock.GetProductByIdAsync(It.IsAny<Guid?>());

            Assert.NotNull(result);
            Assert.Equal("Product 1", result.Name);
        }

        [Fact]
        public async Task RegisterNewProductAsync_ShouldCallRepositoryMethod()
        {
            var product = new Product("New Product", "New Description", 15.0m, "http://newimage.com", ProductCategory.SideDish);

            await _productUseCaseMock.RegisterNewProductAsync(product);

            _mockProductRepository.Verify(repo => repo.RegisterProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldCallRepositoryMethod()
        {
            var product = new Product("Updated Product", "Updated Description", 25.0m, "http://updateimage.com", ProductCategory.SideDish);

            await _updateProductUseCaseMock.UpdateProductAsync(product);

            _mockProductRepository.Verify(repo => repo.UpdateProductAsync(product), Times.Once);
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