using StackFood.Application.Interfaces.Repositories;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Customers.GetByCpf
{
    public class GetByCpfCustomerUseCase : IGetByCpfCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        public GetByCpfCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer?> GetByCpfAsync(string cpf)
        {
            return await _repository.GetByCpfAsync(cpf);
        }
    }
}
