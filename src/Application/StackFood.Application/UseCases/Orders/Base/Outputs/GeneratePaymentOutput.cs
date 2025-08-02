using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackFood.Application.UseCases.Orders.Base.Outputs
{
    public class GeneratePaymentOutput
    {
        public string QrCode { get; set; } = string.Empty;
    }
}
