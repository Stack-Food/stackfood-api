using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task RegisterAsync(Customer customer);
        Task<Customer?> GetByCpfAsync(string cpf);
    }
}
