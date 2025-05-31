using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StackFood.API.Controllers;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;
using StackFood.API.Requests.Products;
using StackFood.Application.UseCases.Products.Create;
using StackFood.Application.UseCases.Products.Delete;
using StackFood.Application.UseCases.Products.GetAll;
using StackFood.Application.UseCases.Products.GetById;
using StackFood.Application.UseCases.Products.Update;

namespace StackFood.UnitTests.Api.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;

        private readonly Mock<ICreateProductUseCase> _productUseCaseMock;
        private readonly Mock<IUpdateProductUseCase> _updateProductUseCaseMock;
        private readonly Mock<IGetAllProductUseCase> _getAllProductUseCaseMock;
        private readonly Mock<IGetByIdProductUseCase> _getByIdProductUseCaseMock;
        private readonly Mock<IDeleteProductUseCase> _deleteProductUseCaseMock;


        public ProductControllerTests()
        {
            _productUseCaseMock = new Mock<ICreateProductUseCase>();
            _updateProductUseCaseMock = new Mock<IUpdateProductUseCase>();
            _getAllProductUseCaseMock = new Mock<IGetAllProductUseCase>();
            _getByIdProductUseCaseMock = new Mock<IGetByIdProductUseCase>();
            _deleteProductUseCaseMock = new Mock<IDeleteProductUseCase>();

            _controller = new ProductController(_productUseCaseMock.Object, _updateProductUseCaseMock.Object, _getAllProductUseCaseMock.Object, _getByIdProductUseCaseMock.Object, _deleteProductUseCaseMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnOk_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product> { new("Coca", "Drink", 5.5m, "url", ProductCategory.Drink) };
            _getAllProductUseCaseMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returned = okResult.Value.Should().BeAssignableTo<IEnumerable<Product>>().Subject;

            returned.Should().HaveCount(1);
            _getAllProductUseCaseMock.Verify(s => s.GetAllProductsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnNotFound_WhenNoProductFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _getAllProductUseCaseMock
                .Setup(s => s.GetAllProductsAsync())
                .ReturnsAsync([]);

            // Act
            var result = await _controller.GetProducts(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk_WhenProductFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product("X-Burguer", "Sanduíche", 10, "img", ProductCategory.Sandwich);
            _getByIdProductUseCaseMock.Setup(s => s.GetProductByIdAsync(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProducts(id);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returned = okResult.Value.Should().BeOfType<Product>().Subject;

            returned.Name.Should().Be("X-Burguer");
            _getByIdProductUseCaseMock.Verify(s => s.GetProductByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnOk_WithCreatedProduct()
        {
            // Arrange
            var request = new CreateProductRequest("Suco", "Natural", 7, "img", (int)ProductCategory.Drink);

            // Act
            var result = await _controller.CreateProduct(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var product = okResult.Value.Should().BeOfType<Product>().Subject;

            product.Name.Should().Be("Suco");
            product.Price.Should().Be(7);
            _productUseCaseMock.Verify(s => s.RegisterNewProductAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _getByIdProductUseCaseMock.Setup(s => s.GetProductByIdAsync(id)).ReturnsAsync((Product?)null);

            // Act
            var result = await _controller.DeleteProduct(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product("Pepsi", "Drink", 6, "img", ProductCategory.Drink);
            _getByIdProductUseCaseMock.Setup(s => s.GetProductByIdAsync(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.DeleteProduct(id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _deleteProductUseCaseMock.Verify(s => s.DeleteProductAsync(id), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var request = new UpdateProductRequest(Guid.NewGuid(), "New", "Desc", 10, "img", 1);
            _getByIdProductUseCaseMock.Setup(s => s.GetProductByIdAsync(request.Id)).ReturnsAsync((Product?)null);

            // Act
            var result = await _controller.UpdateProduct(request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var product = new Product("Old", "Old Desc", 5, "img", ProductCategory.Sandwich);
            var request = new UpdateProductRequest(Guid.NewGuid(), "Updated", "Updated Desc", 10, "img", (int)ProductCategory.Sandwich);

            _getByIdProductUseCaseMock.Setup(s => s.GetProductByIdAsync(request.Id)).ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateProduct(request);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _updateProductUseCaseMock.Verify(s => s.UpdateProductAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}