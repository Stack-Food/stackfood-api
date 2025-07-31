using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Products.GetAll
{
    public interface IGetAllProductUseCase
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(ProductCategory value);
    }
}
