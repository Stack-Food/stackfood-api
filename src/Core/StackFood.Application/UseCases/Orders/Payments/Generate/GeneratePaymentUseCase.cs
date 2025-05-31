using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;
using StackFood.Application.Interfaces.ExternalsServices;
using StackFood.Application.Common;
using StackFood.Domain.Entities;

namespace StackFood.Application.UseCases.Orders.Payments.Generate
{
    public class GeneratePaymentUseCase : IGeneratePaymentUseCase
    {
        public readonly IOrderRepository _orderRepository;
        public readonly ICustomerRepository _customerRepository;
        public readonly IMercadoPagoApiService _mercadoPagoApiService;

        public GeneratePaymentUseCase(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IMercadoPagoApiService mercadoPagoApiService)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _mercadoPagoApiService = mercadoPagoApiService;
        }

        public async Task<Result> GeneratePaymentAsync(GeneratePaymentInput input)
        {
            var order = await _orderRepository.GetByIdAsync(input.OrderId);

            if (order == null)
            {
                 return Result.Failure("Pedido não encontrado.");
            }

            var customer = order.Customer;

            if (order.Payment is not null)
            {
                 return Result.Failure("Pagamento já foi gerado para este pedido.");
            }

            if (customer != null)
            {
                customer = await _customerRepository.GetByIdAsync(customer.Id);
            }

            var (paymentExternalId, qrCode) = await _mercadoPagoApiService.GeneratePaymentAsync(
                input.Type,
                order,
                customer);
            if (paymentExternalId is null)
            {
                 return Result.Failure("Falha ao criar pagamento.");
            }

            if (order.Payment != null)
            {
                return Result.Failure("Pagamento já realizado.");
            }

            order.GeneratePayment(input.Type, paymentExternalId.Value, qrCode);

            await _orderRepository.AddPaymentAsync(order.Payment);
            await _orderRepository.SaveAsync();
            return Result.Success();
        }
    }
}
