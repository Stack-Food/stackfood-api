using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackFood.Application.UseCases.Products.Delete
{
    public interface IDeleteProductUseCase
    {
        Task DeleteProductAsync(Guid? id);
    }
}
