using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Products.Update
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductRepository _repository;

        public UpdateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateProductAsync(product);
        }
    }
}
