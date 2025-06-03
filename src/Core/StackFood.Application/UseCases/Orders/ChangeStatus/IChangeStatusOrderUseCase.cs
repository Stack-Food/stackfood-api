using StackFood.Application.Common;
using StackFood.Application.UseCases.Orders.ChangeStatus.inputs;

namespace StackFood.Application.UseCases.Orders.ChangeStatus
{
    public interface IChangeStatusOrderUseCase
    {
        Task<Result> ChangeStatusOrderAsync(ChangeStatusInput input);
    }
}