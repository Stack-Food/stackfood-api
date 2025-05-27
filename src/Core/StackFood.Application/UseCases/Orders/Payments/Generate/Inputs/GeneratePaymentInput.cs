using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.Payments.Generate.Inputs
{
    public  class GeneratePaymentInput
    {
        public Guid OrderId { get; set; }
        public PaymentType Type { get; set; }
    }
}
