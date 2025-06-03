using StackFood.Application.Common;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Customers.Create
{
    public interface ICreateCustomerUseCase
    {
        Task<Result> CreateCustomerAsync(Customer customer);
    }
}
