using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.GetAll
{
    public class GetAllOrderUseCase : IGetAllOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;

        public GetAllOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderOutput>> GetAllOrderAsync(OrderStatus? status)
        {
            var orders = await _orderRepository.GetAllAsync(status);
            return OrderOutputMapper.Map(orders);
        }

    }
}
