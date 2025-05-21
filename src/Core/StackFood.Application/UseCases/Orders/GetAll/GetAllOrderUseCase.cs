using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;

namespace StackFood.Application.UseCases.Orders.GetAll
{
    public class GetAllOrderUseCase : IGetAllOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;

        public GetAllOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderOutput>> GetAllOrderAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return OrderOutputMapper.Map(orders);
        }
    }
}
