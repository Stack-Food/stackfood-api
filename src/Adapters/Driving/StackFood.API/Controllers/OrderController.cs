using Microsoft.AspNetCore.Mvc;
using StackFood.API.Mappers.Orders.CreateOrder;
using StackFood.API.Requests.Orders;
using StackFood.API.Requests.Orders.Payment;
using StackFood.Application.UseCases.Orders.Create;
using StackFood.Application.UseCases.Orders.GetAll;
using StackFood.Application.UseCases.Orders.GetById;
using StackFood.Application.UseCases.Orders.Payments.Generate;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;

namespace StackFood.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly ICreateOrderUseCase _orderUseCase;
        public readonly IGetAllOrderUseCase _getAllOrderUseCase;
        public readonly IGetByIdOrderUseCase _getByIdOrderUseCase;
        public readonly IGeneratePaymentUseCase _generatePaymentUseCase;

        public OrderController(
            ICreateOrderUseCase orderUseCase,
            IGetAllOrderUseCase getAllOrderUseCase,
            IGetByIdOrderUseCase getByIdOrderUseCase,
            IGeneratePaymentUseCase generatePaymentUseCase)
        {
            _orderUseCase = orderUseCase;
            _getAllOrderUseCase = getAllOrderUseCase;
            _getByIdOrderUseCase = getByIdOrderUseCase;
            _generatePaymentUseCase = generatePaymentUseCase;
        }

        /// <summary>
        /// Retrieves a list of all orders.
        /// </summary>
        /// <returns>
        /// Returns HTTP 200 (OK) with the list of all orders.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _getAllOrderUseCase.GetAllOrderAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves the details of a specific order by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        /// <returns>
        /// Returns HTTP 200 (OK) with the order details,
        /// or HTTP 404 (Not Found) if the order does not exist.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var order = await _getByIdOrderUseCase.GetByIdOrderAsync(id);
            return Ok(order);
        }

        /// <summary>
        /// Creates a new order with the provided information.
        /// </summary>
        /// <param name="request">The request body containing order details.</param>
        /// <returns>
        /// Returns HTTP 200 (OK) with the created order.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request)
        {
            var input = CreateOrderInputMapper.Map(request);

            var order = await _orderUseCase.CreateOrderAsync(input);
            return Ok(order);
        }

        /// <summary>
        /// Generates a payment for a specific order.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        /// <param name="request">The request body containing payment details.</param>
        /// <returns>
        /// Returns HTTP 204 (No Content) on successful payment generation.
        /// </returns>
        [HttpPut("{id}/payment")]
        public async Task GeneratePayment([FromRoute] Guid id, [FromBody] GeneratePaymentRequest request)
        {

            var input = new GeneratePaymentInput
            {
                OrderId = id,
                Type = request.Type
            };

             await _generatePaymentUseCase.GeneratePaymentAsync(input);
        }
    }
}
