using Application.UseCases.Products.AddProduct;
using FluentValidation.TestHelper;

namespace ProductApiTests.AddProduct;

public class AddProductCommandValidatorTests
{
    private readonly AddProductCommandValidator _validator;

    public AddProductCommandValidatorTests()
    {
        _validator = new AddProductCommandValidator();
    }

    [Fact]
    public void Validate_ValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new AddProductCommand
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Colour = "Red",
            Price = 99.99m
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_MissingName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new AddProductCommand
        {
            Name = string.Empty,
            Description = "Valid Description",
            Colour = "Red",
            Price = 99.99m
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Validate_NegativePrice_ShouldHaveValidationError()
    {
        // Arrange
        var command = new AddProductCommand
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Colour = "Red",
            Price = -1m
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Price);
    }
}
