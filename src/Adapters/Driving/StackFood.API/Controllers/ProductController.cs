using Microsoft.AspNetCore.Mvc;
using StackFood.Application.Interfaces.Services;
using StackFood.Application.Services;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _productService.GetAllProductAsync();
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            var product = await _productService.GetProductByNameAsync(name);
            if (product == null) return NotFound();
            return Ok(product);
        }
    }

}
