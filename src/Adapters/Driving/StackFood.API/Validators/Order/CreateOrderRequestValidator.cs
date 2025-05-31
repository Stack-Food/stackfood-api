using FluentValidation;
using StackFood.API.Requests.Orders;

namespace StackFood.API.Validators.Order
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("At least one product is required.");                
        }
    }
}
