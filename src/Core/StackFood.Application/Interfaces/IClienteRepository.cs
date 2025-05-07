using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer cliente);
        Task<Customer?> GetByCpfAsync(string cpf);
    }
}