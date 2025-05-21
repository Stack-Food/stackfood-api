using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Base.Mappers;
using StackFood.Application.UseCases.Orders.Base.Outputs;

namespace StackFood.Application.UseCases.Orders.GetById
{
    public class GetByIdOrderUseCase : IGetByIdOrderUseCase
    {
        public readonly IOrderRepository _orderRepository;

        public GetByIdOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderOutput> GetByIdOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return OrderOutputMapper.Map(order);
        }
    }
}
