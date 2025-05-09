using Microsoft.EntityFrameworkCore;
using StackFood.Application.Interfaces.Repositories;
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

        public async Task RegisterAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetByCpfAsync(string cpf)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }
    }
}
