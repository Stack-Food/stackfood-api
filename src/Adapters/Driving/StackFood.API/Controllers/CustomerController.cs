using Microsoft.AspNetCore.Mvc;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequest request)
        {
            var customer = new Customer(request.Name, request.Email, request.Cpf);
            await _customerService.RegisterAsync(customer);
            return Ok(customer);
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf)
        {
            var customer = await _customerService.GetByCpfAsync(cpf);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
    }

    public record CustomerRequest(string Name, string Email, string Cpf);
}
