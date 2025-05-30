using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.ChangeStatus.inputs;
using StackFood.Application.UseCases.Orders.Payments.Check;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.ChangeStatus
{
    public class ChangeStatusOrderUseCase : IChangeStatusOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeStatusOrderUseCase(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task ChangeStatusOrderAsync(ChangeStatusInput input)
        {
            var order = await _orderRepository.GetByIdAsync(input.OrderId);
            if (order == null)
            {
                return;
            }

            switch (input.Status)
            {
                case OrderStatus.Ready:
                    order.Ready();
                    break;
                case OrderStatus.Finalized:
                    order.Finalized();
                    break;
                default:
                    throw new InvalidOperationException("Status inválido para o pedido.");
            }    

            await _orderRepository.SaveAsync();
        }
    }
}