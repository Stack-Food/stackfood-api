using Moq;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Services;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Tests.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
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

            var result = await _productService.GetAllProductsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("Baicon burger", result[0].Name);
        }

        [Fact]
        public async Task GetProductByFilterAsync_ShouldReturnProduct_WhenExists()
        {
            var product = new Product("Product 1", "Description", 10.0m, "http://image.com", ProductCategory.Sandwich);
            _mockProductRepository.Setup(repo => repo.GetProductByFilterAsync("Product 1", It.IsAny<Guid?>())).ReturnsAsync(product);

            var result = await _productService.GetProductByFilterAsync("Product 1", null);

            Assert.NotNull(result);
            Assert.Equal("Product 1", result.Name);
        }

        [Fact]
        public async Task RegisterNewProductAsync_ShouldCallRepositoryMethod()
        {
            var product = new Product("New Product", "New Description", 15.0m, "http://newimage.com", ProductCategory.SideDish);

            await _productService.RegisterNewProductAsync(product);

            _mockProductRepository.Verify(repo => repo.RegisterProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldCallRepositoryMethod()
        {
            var product = new Product("Updated Product", "Updated Description", 25.0m, "http://updateimage.com", ProductCategory.SideDish);

            await _productService.UpdateProductAsync(product);

            _mockProductRepository.Verify(repo => repo.UpdateProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallRepositoryMethod()
        {
            Guid productId = Guid.NewGuid();

            await _productService.DeleteProductAsync(productId);

            _mockProductRepository.Verify(repo => repo.DeleteProductByIdAsync(productId), Times.Once);
        }
    }
}