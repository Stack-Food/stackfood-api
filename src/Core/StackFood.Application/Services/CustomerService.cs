using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;

namespace StackFood.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task RegisterAsync(Customer customer)
        {
            await _repository.RegisterAsync(customer);
        }

        public async Task<Customer?> GetByCpfAsync(string cpf)
        {
            return await _repository.GetByCpfAsync(cpf);
        }
    }
}