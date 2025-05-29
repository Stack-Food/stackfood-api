using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Products.Create
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductRepository _repository;

        public CreateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterNewProductAsync(Product product)
        {
            await _repository.RegisterProductAsync(product);
        }
    }
}
