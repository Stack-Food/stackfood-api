using Microsoft.AspNetCore.Mvc;
using StackFood.Application.Interfaces;
using StackFood.Domain.Entities;

namespace StackFood.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteRequest request)
        {
            var cliente = new Cliente(request.Nome, request.Email, request.Cpf);
            await _clienteRepository.AdicionarAsync(cliente);
            return Ok(cliente);
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> ObterPorCpf(string cpf)
        {
            var cliente = await _clienteRepository.ObterPorCpfAsync(cpf);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
    }

    public record ClienteRequest(string Nome, string Email, string Cpf);
}
