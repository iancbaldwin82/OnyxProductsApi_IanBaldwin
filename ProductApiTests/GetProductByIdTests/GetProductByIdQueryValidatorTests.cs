using Application.UseCases.Products.GetProductById;
using FluentValidation.TestHelper;

namespace ProductApiTests.GetProductByIdTests;

public class GetProductsByIdQueryValidatorTests
{
    private readonly GetProductByIdQueryValidator _validator;

    public GetProductsByIdQueryValidatorTests()
    {
        _validator = new GetProductByIdQueryValidator();
    }

    [Fact]
    public void Validate_IdIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetProductByIdQuery { Id = Guid.Empty };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Id)
              .WithErrorMessage("Product Id is required.");
    }

    [Fact]
    public void Validate_IdIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetProductByIdQuery { Id = Guid.NewGuid() };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.Id);
    }
}
