using Microsoft.AspNetCore.Mvc;
using StackFood.API.Mappers.Orders.CreateOrder;
using StackFood.API.Requests.Orders;
using StackFood.Application.UseCases.Orders.CreateOrder;

namespace StackFood.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly ICreateOrderUseCase _orderUseCase;

        public OrderController(ICreateOrderUseCase orderUseCase)
        {
            _orderUseCase = orderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            var input = CreateOrderInputMapper.Map(request);

            var order = await _orderUseCase.CreateOrderAsync(input);
            return Ok(order);
        }
    }
}
