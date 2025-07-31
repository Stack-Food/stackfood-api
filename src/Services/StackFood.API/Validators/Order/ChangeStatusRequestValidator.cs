using FluentValidation;
using StackFood.API.Requests.Orders;

namespace StackFood.API.Validators.Order
{
    public class ChangeStatusRequestValidator : AbstractValidator<ChangeStatusRequest>
    {
        public ChangeStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("The reported status is invalid.");
        }
    }
}
