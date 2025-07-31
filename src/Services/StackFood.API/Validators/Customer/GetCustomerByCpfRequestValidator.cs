using FluentValidation;
using StackFood.API.Requests.Customers;

namespace StackFood.API.Validators.Customer
{
    public class GetCustomerByCpfRequestValidator : AbstractValidator<GetCustomerByCpfRequest>
    {
        public GetCustomerByCpfRequestValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF is required.")
                .Matches(@"^\d{11}$").WithMessage("CPF must contain exactly 11 numeric digits.");
        }
    }
}
