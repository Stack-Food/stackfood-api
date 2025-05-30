using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Interfaces.Services;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;

namespace StackFood.Application.UseCases.Orders.Payments.Check
{
    public class CheckPaymentUseCase : ICheckPaymentUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMercadoPagoApiService _mercadoPagoApiService;

        public CheckPaymentUseCase(
            IOrderRepository orderRepository,
            IMercadoPagoApiService mercadoPagoApiService)
        {
            _orderRepository = orderRepository;
            _mercadoPagoApiService = mercadoPagoApiService;
        }

        public async Task CheckPaymentAsync()
        {
            var pendingPaymentOrders = await _orderRepository.GetPendingPaymentOrdersAsync();

            foreach (var order in pendingPaymentOrders)
            {
                var paymentStatus = await _mercadoPagoApiService.GetPaymentStatusAsync(order);

                if (paymentStatus == PaymentStatus.Paid)
                {
                    order.Paid();
                }
                else if (paymentStatus == PaymentStatus.Cancelled)
                {
                    order.Cancelled();
                }

                await _orderRepository.SaveAsync();
            }
        }
    }
}