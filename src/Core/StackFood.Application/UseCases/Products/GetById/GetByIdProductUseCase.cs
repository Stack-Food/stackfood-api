using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Products.GetById
{
    public class GetByIdProductUseCase : IGetByIdProductUseCase
    {
        private readonly IProductRepository _repository;

        public GetByIdProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetProductByIdAsync(Guid? id)
        {
            return await _repository.GetProductByIdAsync(id);
        }
    }
}
