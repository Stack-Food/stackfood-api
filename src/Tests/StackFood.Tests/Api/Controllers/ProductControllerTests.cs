using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StackFood.API.Controllers;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;
using StackFood.Application.Interfaces.Services;
using Xunit;
using StackFood.API.Requests.Products;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _controller = new ProductController(_productServiceMock.Object);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnOk_WhenProductsExist()
    {
        // Arrange
        var products = new List<Product> { new("Coca", "Drink", 5.5m, "url", ProductCategory.Drink) };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAllProducts();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var returned = okResult.Value.Should().BeAssignableTo<IEnumerable<Product>>().Subject;

        returned.Should().HaveCount(1);
        _productServiceMock.Verify(s => s.GetAllProductsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnBadRequest_WhenIdIsNull()
    {
        // Act
        var result = await _controller.GetProducts(null);

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.Value.Should().Be("É necessário fornecer parâmetros: id.");
    }

    [Fact]
    public async Task GetProducts_ShouldReturnNotFound_WhenNoProductFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productServiceMock
            .Setup(s => s.GetAllProductsAsync())
            .ReturnsAsync(new List<Product>());

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
        _productServiceMock.Setup(s => s.GetProductByFilterAsync(id)).ReturnsAsync(product);

        // Act
        var result = await _controller.GetProducts(id);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var returned = okResult.Value.Should().BeOfType<Product>().Subject;

        returned.Name.Should().Be("X-Burguer");
        _productServiceMock.Verify(s => s.GetProductByFilterAsync(id), Times.Once);
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
        _productServiceMock.Verify(s => s.RegisterNewProductAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productServiceMock.Setup(s => s.GetProductByFilterAsync(id)).ReturnsAsync((Product?)null);

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
        _productServiceMock.Setup(s => s.GetProductByFilterAsync(id)).ReturnsAsync(product);

        // Act
        var result = await _controller.DeleteProduct(id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        _productServiceMock.Verify(s => s.DeleteProductAsync(id), Times.Once);
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var request = new UpdateProductRequest(Guid.NewGuid(), "New", "Desc", 10, "img", 1);
        _productServiceMock.Setup(s => s.GetProductByFilterAsync(request.Id)).ReturnsAsync((Product?)null);

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

        _productServiceMock.Setup(s => s.GetProductByFilterAsync(request.Id)).ReturnsAsync(product);

        // Act
        var result = await _controller.UpdateProduct(request);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        _productServiceMock.Verify(s => s.UpdateProductAsync(It.IsAny<Product>()), Times.Once);
    }
}