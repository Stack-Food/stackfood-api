using StackFood.Application.Common;
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

        public async Task<Result> CreateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _repository.GetByCpfAsync(customer.Cpf);
            if (existingCustomer != null)
            {
                return Result.Failure("Cliente já cadastrado.");
            }

            await _repository.CreateAsync(customer);

            return Result.Success();
        }
    }
}
