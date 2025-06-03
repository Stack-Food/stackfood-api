using StackFood.Application.UseCases.Orders.Base.Outputs;

namespace StackFood.Application.UseCases.Orders.GetById
{
    public interface IGetByIdOrderUseCase
    {
        Task<OrderOutput> GetByIdOrderAsync(Guid id);
    }
}