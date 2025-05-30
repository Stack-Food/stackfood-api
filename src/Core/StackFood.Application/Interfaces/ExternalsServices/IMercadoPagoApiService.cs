using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.Interfaces.ExternalsServices
{
    public interface IMercadoPagoApiService
    {
        Task<(long? paymentExternalId, string qrCodeUrl)> GeneratePaymentAsync(
            PaymentType type,
            StackFood.Domain.Entities.Order order,
            StackFood.Domain.Entities.Customer custumer);
        Task<PaymentStatus> GetPaymentStatusAsync(Order order);
    }
}