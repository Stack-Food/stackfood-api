using Microsoft.AspNetCore.Mvc;
using StackFood.API.Requests.Products;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products available in the catalog.
        /// </summary>
        /// <returns>
        /// Returns HTTP 200 (OK) with the list of all products,
        /// or HTTP 404 (Not Found) if no products are found.
        /// </returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _productService.GetAllProductsAsync();
            if (product == null) return NotFound();
            return Ok(product);
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
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] Guid? id = null)
        {
            if (!id.HasValue)    {
                return BadRequest("É necessário fornecer parâmetros: id.");
            }
            var products = await _productService.GetProductByFilterAsync(id); 
            
            if (products == null) return NotFound();
            
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
            await _productService.RegisterNewProductAsync(product);
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
            var product = await _productService.GetProductByFilterAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
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
            var existingProduct = await _productService.GetProductByFilterAsync(request.Id); 
            
            if (existingProduct == null) {
                return NotFound(); 
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

            await _productService.UpdateProductAsync(existingProduct);

            return NoContent();
        }        
    }
}
