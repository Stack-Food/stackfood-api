using Microsoft.AspNetCore.Mvc;
using StackFood.Application.Interfaces;
using StackFood.Domain.Entities;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerRequest request)
        {
            var customer = new Customer(request.Name, request.Email, request.Cpf);
            await _customerRepository.AddAsync(customer);
            return Ok(customer);
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var customer = await _customerRepository.GetByCpfAsync(cpf);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
    }

    public record CustomerRequest(string Name, string Email, string Cpf);
}
