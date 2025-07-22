using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Products.Create
{
    public interface ICreateProductUseCase
    {
        Task RegisterNewProductAsync(Product product);
    }
}
