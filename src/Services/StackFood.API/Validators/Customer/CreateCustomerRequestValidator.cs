using FluentValidation;
using StackFood.API.Requests.Customers;

namespace StackFood.API.Validators.Customer
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Cpf)
                .NotEmpty().Matches(@"^\d{11}$").WithMessage("CPF deve conter 11 dígitos.");
        }
    }
}
