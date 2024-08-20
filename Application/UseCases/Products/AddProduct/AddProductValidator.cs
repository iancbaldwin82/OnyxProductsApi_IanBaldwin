using FluentValidation;

namespace Application.UseCases.Products.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .Length(1, 500).WithMessage("Product description must be between 1 and 500 characters.");

        RuleFor(x => x.Colour)
            .NotEmpty().WithMessage("Product colour is required.")
            .Length(1, 50).WithMessage("Product colour must be between 1 and 50 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
    }
}