using StackFood.Application.Common;
using StackFood.Application.UseCases.Orders.Base.Outputs;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;

namespace StackFood.Application.UseCases.Orders.Payments.Generate
{
   public interface IGeneratePaymentUseCase
    {
        Task<Result<GeneratePaymentOutput>> GeneratePaymentAsync(GeneratePaymentInput input);
    }
}
