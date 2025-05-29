using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Customers.Create
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _repository.CreateAsync(customer);
        }
    }
}
