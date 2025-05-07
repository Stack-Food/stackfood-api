using Microsoft.EntityFrameworkCore;
using StackFood.Application.Interfaces;
using StackFood.Domain.Entities;

namespace StackFood.Infra.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer cliente)
        {
            await _context.Customers.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetByCpfAsync(string cpf)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }
    }
}
