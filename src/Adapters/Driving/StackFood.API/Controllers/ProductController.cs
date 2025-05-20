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
        public async Task<IActionResult> GetProducts([FromQuery] string name = null, [FromQuery] Guid? id = null)
        {
            if (string.IsNullOrEmpty(name) && !id.HasValue)    {
                return BadRequest("É necessário fornecer pelo menos um dos parâmetros: name ou id.");
            }
            var products = await _productService.GetProductByFilterAsync(name, id); 
            
            if (products == null) return NotFound();
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            var product = new Product(request.Name, request.Desc, request.Price, request.Img, (ProductCategory)Enum.ToObject(typeof(ProductCategory), request.Category));
            await _productService.RegisterNewProductAsync(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest request) 
        { 
            var product = await _productService.GetProductByFilterAsync(request.Name, null);
            
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
            var product = await _productService.GetProductByFilterAsync(null, id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        public record ProductRequest(string Name, string Desc, decimal Price, string Img, int Category);

    }

}
