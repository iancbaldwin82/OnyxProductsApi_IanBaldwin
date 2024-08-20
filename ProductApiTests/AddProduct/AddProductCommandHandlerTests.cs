using Application.Interfaces;
using Application.UseCases.Products.AddProduct;
using Domain;
using MediatR;
using Moq;

namespace ProductApiTests.AddProduct;

public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductWriteRepository> _mockProductWriteRepository;
    private readonly AddProductCommandHandler _handler;

    public AddProductCommandHandlerTests()
    {
        _mockProductWriteRepository = new Mock<IProductWriteRepository>();
        _handler = new AddProductCommandHandler(_mockProductWriteRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddProduct()
    {
        // Arrange
        var command = new AddProductCommand
        {
            Name = "Test Product",
            Description = "Test Description",
            Colour = "Red",
            Price = 99.99m
        };

        _mockProductWriteRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockProductWriteRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        Assert.Equal(Unit.Value, result);
    }
}