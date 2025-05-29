using FluentValidation;
using StackFood.API.Requests.Products;

namespace StackFood.API.Validators.Product
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must be at most 100 characters.");

            RuleFor(x => x.Desc)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(300).WithMessage("Description must be at most 300 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Img)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _))
                .WithMessage("Image must be a valid URL.");
        }
    }
}
