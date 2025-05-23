using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;

namespace StackFood.Application.UseCases.Orders.Payments.Generate
{
   public interface IGeneratePaymentUseCase
    {
        Task GeneratePaymentAsycn(GeneratePaymentInput input);
    }
}
