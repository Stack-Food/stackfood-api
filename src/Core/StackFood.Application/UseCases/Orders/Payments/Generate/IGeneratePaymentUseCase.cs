using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;

namespace StackFood.Application.UseCases.Orders.Payments.Generate
{
   public interface IGeneratePaymentUseCase
    {
        Task GeneratePaymentAsync(GeneratePaymentInput input);
    }
}
