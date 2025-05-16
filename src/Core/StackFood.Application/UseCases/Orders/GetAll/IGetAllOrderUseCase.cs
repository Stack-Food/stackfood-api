using StackFood.Application.UseCases.Orders.Base.Outputs;

namespace StackFood.Application.UseCases.Orders.GetAll
{
    public interface IGetAllOrderUseCase
    {
        Task<IEnumerable<OrderOutput>> GetAllOrderAsync();
    }
}