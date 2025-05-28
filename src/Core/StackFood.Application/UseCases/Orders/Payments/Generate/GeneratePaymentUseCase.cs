
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Orders.Payments.Generate.Inputs;
using StackFood.Domain.Entities;
using StackFood.Domain.Enums;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;

namespace StackFood.Application.UseCases.Orders.Payments.Generate
{
    public class GeneratePaymentUseCase : IGeneratePaymentUseCase
    {
        public readonly IOrderRepository _orderRepository;

        public readonly ICustomerRepository _customerRepository;

        public GeneratePaymentUseCase(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
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
            {
                PaymentType.Pix => "pix",
                _ => throw new ArgumentOutOfRangeException(nameof(input), "Tipo de pagamento não suportado.")
            };


            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = order.TotalPrice,
                Description = "Descrição da compra",
                PaymentMethodId = paymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email = "stackFood@fiap.com",
                    FirstName = custumer.Name
                }
            };

            var client = new PaymentClient();
            var payment = await client.CreateAsync(paymentRequest);

            order.GeneratePayment(
                payment.PointOfInteraction.TransactionData.QrCode,
                payment.Id.ToString() // ou payment.Id, dependendo do tipo
            );
            await _orderRepository.SaveAsync();

        }
    }
}
