using StackFood.Application.UseCases.Orders.CreateOrder.Inputs;
using StackFood.Application.UseCases.Orders.CreateOrder.Outputs;

namespace StackFood.Application.UseCases.Orders.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task<CreateOrderOutput> CreateOrderAsync(CreateOrderInput input);
    }
}