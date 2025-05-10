using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;

namespace StackFood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByFilterAsync(string name, Guid? id)
        {
            return await _productRepository.GetProductByFilterAsync(name, id);
        }

        public async Task RegisterNewProductAsync(Product product)
        {
           await _productRepository.RegisterProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(Guid? id)
        {
            await _productRepository.DeleteProductByIdAsync(id);
        }
    }
}
