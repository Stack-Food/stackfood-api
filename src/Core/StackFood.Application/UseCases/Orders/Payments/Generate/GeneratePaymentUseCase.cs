using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;
using StackFood.Application.Interfaces.ExternalsServices;

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

        public async Task GeneratePaymentAsync(GeneratePaymentInput input)
        {
            var order = await _orderRepository.GetByIdAsync(input.OrderId);
            if (order == null)
            {
                return;
            }


            var custumer = await _customerRepository.GetByIdAsync(order.Customer.Id);


            var paymentMethodId = input.Type switch
            
            if (order.Payment is not null)
            
            {
                throw new InvalidOperationException("Pagamento já foi gerado para este pedido.");
            }

            var custumer = await _customerRepository.GetByIdAsync(order.Customer.Id);

            var (paymentExternalId, qrCode) = await _mercadoPagoApiService.GeneratePaymentAsync(
                input.Type,
                order,
                custumer);
            if (paymentExternalId is null)
            {
                throw new InvalidOperationException("Falha ao criar pagamento.");
            }

            order.GeneratePayment(input.Type, paymentExternalId.Value, qrCode);

            order.GeneratePayment(
                payment.PointOfInteraction.TransactionData.QrCode,
                payment.Id.ToString() // ou payment.Id, dependendo do tipo
            );
            await _orderRepository.AddPaymentAsync(order.Payment);
        }
    }
}
