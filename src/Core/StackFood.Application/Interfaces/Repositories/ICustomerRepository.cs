using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task<Customer?> GetByCpfAsync(string cpf);
        Task<Customer> GetByIdAsync(Guid id);
    }
}