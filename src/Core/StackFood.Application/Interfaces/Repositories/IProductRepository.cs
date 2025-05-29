using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task DeleteProductByIdAsync(Guid? id);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetByIdAsync(Guid productId);
        Task<Product> GetProductByFilterAsync(Guid? id);
        Task RegisterProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
