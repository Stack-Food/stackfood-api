using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Products.GetAll
{
    public class GetAllProductUseCase : IGetAllProductUseCase
    {
        private readonly IProductRepository _repository;

        public GetAllProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(ProductCategory value)
        {
            return await _repository.GetAllProductsByCategoryAsync(value);
        }
    }
}
