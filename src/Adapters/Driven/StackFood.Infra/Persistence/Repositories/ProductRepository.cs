using Microsoft.EntityFrameworkCore;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        
        public async Task<Product> GetByIdAsync(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<Product> GetProductByFilterAsync(Guid? id)
        {
            IQueryable<Product> query = _context.Products;
            if (id.HasValue)
            {
                query = query.Where(p => p.Id == id.Value);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RegisterProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(Guid? id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product != null) {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(); 
            }
        }
    }
}
