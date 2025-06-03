using Microsoft.AspNetCore.Mvc;
using StackFood.API.Requests.Products;
using StackFood.Application.Interfaces.Services;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Products.Create;
using StackFood.Application.UseCases.Products.Delete;
using StackFood.Application.UseCases.Products.GetAll;
using StackFood.Application.UseCases.Products.GetById;
using StackFood.Application.UseCases.Products.Update;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        public readonly ICreateProductUseCase _productUseCase;
        public readonly IUpdateProductUseCase _updateProductUseCase;
        public readonly IGetAllProductUseCase _getAllProductUseCase;
        public readonly IGetByIdProductUseCase _getByIdProductUseCase;
        public readonly IDeleteProductUseCase _deleteProductUseCase;

        public ProductController(ICreateProductUseCase productService, IUpdateProductUseCase updateProductUseCase,
                                 IGetAllProductUseCase getAllProductUseCase, IGetByIdProductUseCase getByIdProductUseCase,
                                 IDeleteProductUseCase deleteProductUseCase)
        {
            _productUseCase = productService;
            _updateProductUseCase = updateProductUseCase;
            _getAllProductUseCase = getAllProductUseCase;
            _getByIdProductUseCase = getByIdProductUseCase;
            _deleteProductUseCase = deleteProductUseCase;
        }

        /// <summary>
        /// Retrieves all products available in the catalog.
        /// </summary>
        /// <returns>
        /// Returns HTTP 200 (OK) with the list of all products,
        /// or HTTP 404 (Not Found) if no products are found.
        /// </returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductCategory? category = null)
        {
            IEnumerable<Product> products;
            if (category.HasValue) {
                products = await _getAllProductUseCase.GetProductsByCategoryAsync(category.Value);
            }
            else {
                products = await _getAllProductUseCase.GetAllProductsAsync();
            }
            if (products == null || !products.Any()) return Ok(Array.Empty<Product>());
            return Ok(products);
        }

        /// <summary>
        /// Retrieves products based on the provided ID filter.
        /// </summary>
        /// <param name="id">Optional product ID to filter the results.</param>
        /// <returns>
        /// Returns HTTP 200 (OK) with the filtered products,
        /// HTTP 400 (Bad Request) if no filter is provided,
        /// or HTTP 404 (Not Found) if no products match the filter.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            var products = await _getByIdProductUseCase.GetProductByIdAsync(id); 
            
            if (products == null) return Ok(Array.Empty<Product>());

            return Ok(products);
        }

        /// <summary>
        /// Creates a new product in the catalog.
        /// </summary>
        /// <param name="request">The request body containing product details.</param>
        /// <returns>
        /// Returns HTTP 200 (OK) with the created product.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = new Product(request.Name, request.Desc, request.Price, request.Img, (ProductCategory)Enum.ToObject(typeof(ProductCategory), request.Category));
            await _productUseCase.RegisterNewProductAsync(product);
            return Ok(product);
        }

        /// <summary>
        /// Deletes a product by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>
        /// Returns HTTP 204 (No Content) if the deletion was successful,
        /// or HTTP 404 (Not Found) if the product was not found.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid? id)
        {
            var product = await _getByIdProductUseCase.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _deleteProductUseCase.DeleteProductAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Updates the information of an existing product.
        /// </summary>
        /// <param name="request">The request body containing updated product information.</param>
        /// <returns>
        /// Returns HTTP 204 (No Content) if the update was successful,
        /// or HTTP 404 (Not Found) if the product was not found.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            var existingProduct = await _getByIdProductUseCase.GetProductByIdAsync(request.Id); 
            
            if (existingProduct == null) {
                return Ok(Array.Empty<Product>()); 
            }

            existingProduct.Update(
                request.Name,
                request.Desc,
                request.Price,
                request.Img, 
                (ProductCategory)Enum.ToObject(typeof(ProductCategory),
                request.Category
                ));

            existingProduct.SetId (request.Id);

            await _updateProductUseCase.UpdateProductAsync(existingProduct);

            return NoContent();
        }        
    }
}
