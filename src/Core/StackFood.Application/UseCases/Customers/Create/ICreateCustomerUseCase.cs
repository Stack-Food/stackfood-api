using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Customers.Create
{
    public interface ICreateCustomerUseCase
    {
        Task CreateCustomerAsync(Customer customer);
    }
}
