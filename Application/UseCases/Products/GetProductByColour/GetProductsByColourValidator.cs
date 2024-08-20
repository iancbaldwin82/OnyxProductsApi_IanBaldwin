using FluentValidation;

namespace Application.UseCases.Products.GetProductsByColour;

public class GetProductsByColourQueryValidator : AbstractValidator<GetProductsByColourQuery>
{
    public GetProductsByColourQueryValidator()
    {
        RuleFor(query => query.Colour)
            .NotEmpty().WithMessage("Colour is required.")
            .MinimumLength(3).WithMessage("Colour must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Colour must be less than 20 characters long.");
    }
}
