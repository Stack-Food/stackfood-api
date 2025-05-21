using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _productService.GetAllProductsAsync();
            if (product == null) return NotFound();
            return Ok(product);
        }

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

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = new Product(request.Name, request.Desc, request.Price, request.Img, (ProductCategory)Enum.ToObject(typeof(ProductCategory), request.Category));
            await _productService.RegisterNewProductAsync(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request) 
        { 
            var product = await _productService.GetProductByFilterAsync(request.Id);
            
            if (product == null) {
                return NotFound(); 
            }
            product = new Product(
                request.Name,
                request.Desc,
                request.Price,
                request.Img,
                (ProductCategory) Enum.ToObject(typeof(ProductCategory), request.Category));

            await _productService.UpdateProductAsync(product); 
            return Ok(product); 
        }

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
        public record CreateProductRequest(string Name, string Desc, decimal Price, string Img, int Category); 
        public record UpdateProductRequest(Guid Id, string Name, string Desc, decimal Price, string Img, int Category);
    }

}
