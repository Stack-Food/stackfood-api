using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.Interfaces.Repositories;

namespace StackFood.Application.UseCases.Products.Delete
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository _repository;

        public DeleteProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteProductAsync(Guid? id)
        {
            await _repository.DeleteProductByIdAsync(id);
        }
    }
}
