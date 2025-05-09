using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task RegisterAsync(Customer customer);
        Task<Customer?> GetByCpfAsync(string cpf);
    }
}