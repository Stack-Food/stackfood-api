using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductByNameAsync(string name);
    }
}
