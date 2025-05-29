using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Customers.GetByCpf
{
    public interface IGetByCpfCustomerUseCase
    {
        Task<Customer?> GetByCpfAsync(string cpf);
    }
}
