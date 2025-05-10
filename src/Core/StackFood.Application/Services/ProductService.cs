using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;

namespace StackFood.Application.Services
{
    public class ProductService : IProductService
    {
        Task<List<Product>> IProductService.GetAllProductAsync()
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductService.GetProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
