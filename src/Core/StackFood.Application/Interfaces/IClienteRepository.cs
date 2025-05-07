using StackFood.Domain.Entities;

namespace StackFood.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarAsync(Cliente cliente);
        Task<Cliente?> ObterPorCpfAsync(string cpf);
    }
}
