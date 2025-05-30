using StackFood.Application.UseCases.Orders.ChangeStatus.inputs;

namespace StackFood.Application.UseCases.Orders.ChangeStatus
{
    public interface IChangeStatusOrderUseCase
    {
        Task ChangeStatusOrderAsync(ChangeStatusInput input);
    }
}