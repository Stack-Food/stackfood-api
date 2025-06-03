using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task DeleteProductByIdAsync(Guid? id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetAllProductsByCategoryAsync(ProductCategory value);
        Task<Product> GetByIdAsync(Guid productId);
        Task<Product> GetProductByIdAsync(Guid? id);
        Task RegisterProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
