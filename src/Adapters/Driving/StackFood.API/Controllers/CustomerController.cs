using Microsoft.AspNetCore.Mvc;
using StackFood.API.Requests.Customers;
using StackFood.Application.UseCases.Customers.Create;
using StackFood.Application.UseCases.Customers.GetByCpf;
using StackFood.Domain.Entities;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerUseCase _createCustomerUseCase;
        private readonly IGetByCpfCustomerUseCase _getByCpfCustomerUseCase;

        public CustomerController(
            ICreateCustomerUseCase createCustomerUseCase, 
            IGetByCpfCustomerUseCase getByCpfCustomerUseCase)
        {
            _createCustomerUseCase = createCustomerUseCase;
            _getByCpfCustomerUseCase = getByCpfCustomerUseCase;
        }

        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="request">The customer data (name, email, and CPF).</param>
        /// <returns>
        /// Returns HTTP 200 (OK) with the created customer, 
        /// or HTTP 400 (Bad Request) if the input is invalid.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
        {
            var customer = new Customer(request.Name, request.Email, request.Cpf);
            var result = await _createCustomerUseCase.CreateCustomerAsync(customer);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(customer);
        }

        /// <summary>
        /// Get a customer by CPF.
        /// </summary>
        /// <param name="request">Request with CPF of the customer to retrieve.</param>
        /// <returns>
        /// Returns HTTP 200 (OK) with the customer data, 
        /// or HTTP 404 (Not Found) if no customer is found with the given CPF.
        /// </returns>
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf([FromRoute] GetCustomerByCpfRequest request)
        {
            var customer = await _getByCpfCustomerUseCase.GetByCpfAsync(request.Cpf);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }
    }    
}
