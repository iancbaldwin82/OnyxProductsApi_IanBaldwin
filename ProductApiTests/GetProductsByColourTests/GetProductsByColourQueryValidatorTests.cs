using Application.UseCases.Products.GetProductsByColour;
using FluentValidation.TestHelper;

namespace ProductApiTests.GetProductsByColourTests;

public class GetProductsByColourQueryValidatorTests
{
    private readonly GetProductsByColourQueryValidator _validator;

    public GetProductsByColourQueryValidatorTests()
    {
        _validator = new GetProductsByColourQueryValidator();
    }

    [Fact]
    public void Validate_ColourIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetProductsByColourQuery { Colour = string.Empty };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Colour)
              .WithErrorMessage("Colour is required.");
    }

    [Fact]
    public void Validate_ColourIsTooShort_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetProductsByColourQuery { Colour = "A" };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Colour)
              .WithErrorMessage("Colour must be at least 3 characters long.");
    }

    [Fact]
    public void Validate_ColourIsTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var query = new GetProductsByColourQuery { Colour = new string('a', 21) };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Colour)
              .WithErrorMessage("Colour must be less than 20 characters long.");
    }

    [Fact]
    public void Validate_ColourIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var query = new GetProductsByColourQuery { Colour = "Red" };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.Colour);
    }
}
