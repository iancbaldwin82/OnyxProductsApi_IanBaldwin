using FluentValidation;

namespace Application.UseCases.Products.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Product Id is required.");
    }
}