using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task DeleteProductAsync(Guid? id);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByFilterAsync(string name, Guid? id);
        Task RegisterNewProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
