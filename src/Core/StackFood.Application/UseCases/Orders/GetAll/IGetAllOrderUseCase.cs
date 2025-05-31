using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.GetAll
{
    public interface IGetAllOrderUseCase
    {
        Task<IEnumerable<OrderOutput>> GetAllOrderAsync(OrderStatus? status);
    }
}