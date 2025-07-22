using FluentValidation;
using StackFood.API.Requests.Orders.Payment;

namespace StackFood.API.Validators.Order
{
    public class GeneratePaymentRequestValidator : AbstractValidator<GeneratePaymentRequest>
    {
        public GeneratePaymentRequestValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Payment type is invalid.");
        }
    }
}
