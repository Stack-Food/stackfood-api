using StackFood.Application.Common;
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

        public async Task<Result> ChangeStatusOrderAsync(ChangeStatusInput input)
        {
            var order = await _orderRepository.GetByIdAsync(input.OrderId);
            if (order == null)
            {
                return Result.Failure("Pedido não encontrado.");
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
                    return Result.Failure("Status inválido para o pedido.");
            }    

            await _orderRepository.SaveAsync();
            return Result.Success();
        }
    }
}