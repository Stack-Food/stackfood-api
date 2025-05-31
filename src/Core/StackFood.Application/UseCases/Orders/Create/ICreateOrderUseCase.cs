using StackFood.Application.Common;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Orders.Create.Inputs;

namespace StackFood.Application.UseCases.Orders.Create
{
    public interface ICreateOrderUseCase
    {
        Task<Result<OrderOutput>> CreateOrderAsync(CreateOrderInput input);
    }
}