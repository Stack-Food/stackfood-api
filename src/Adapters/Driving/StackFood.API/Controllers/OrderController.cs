using Microsoft.AspNetCore.Mvc;
using StackFood.API.Mappers.Orders.CreateOrder;
using StackFood.API.Requests.Orders;
using StackFood.API.Requests.Orders.Payment;
using StackFood.Application.UseCases.Orders.Create;
using StackFood.Application.UseCases.Orders.GetAll;
using StackFood.Application.UseCases.Orders.GetById;

namespace StackFood.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly ICreateOrderUseCase _orderUseCase;
        public readonly IGetAllOrderUseCase _getAllOrderUseCase;
        public readonly IGetByIdOrderUseCase _getByIdOrderUseCase;

        public OrderController(
            ICreateOrderUseCase orderUseCase,
            IGetAllOrderUseCase getAllOrderUseCase,
            IGetByIdOrderUseCase getByIdOrderUseCase)
        {
            _orderUseCase = orderUseCase;
            _getAllOrderUseCase = getAllOrderUseCase;
            _getByIdOrderUseCase = getByIdOrderUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _getAllOrderUseCase.GetAllOrderAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var order = await _getByIdOrderUseCase.GetByIdOrderAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request)
        {
            var input = CreateOrderInputMapper.Map(request);

            var order = await _orderUseCase.CreateOrderAsync(input);
            return Ok(order);
        }

        [HttpPut("{id}/payment")]
        public async Task<IActionResult> GeneratePayament([FromRoute] Guid id, [FromBody] GeneratePaymentRequest request)
        {
            var orders = await _getAllOrderUseCase.GetAllOrderAsync();
            return Ok(orders);
        }
    }
}
