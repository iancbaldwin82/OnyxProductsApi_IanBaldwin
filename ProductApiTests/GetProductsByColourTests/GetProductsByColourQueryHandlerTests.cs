using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Products.GetProductsByColour;
using AutoMapper;
using Domain;
using Moq;

namespace ProductApiTests.GetProductsByColourTests;

public class GetProductsByColourQueryHandlerTests
{
    private readonly Mock<IProductReadRepository> _mockProductReadRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetProductsByColourQueryHandler _handler;

    public GetProductsByColourQueryHandlerTests()
    {
        _mockProductReadRepository = new Mock<IProductReadRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetProductsByColourQueryHandler(_mockProductReadRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidColour_ReturnsMappedProductDTOs()
    {
        // Arrange
        var query = new GetProductsByColourQuery { Colour = "Red" };
        var products = new List<Product>
        {
            new() { Name = "Envelopes", Description = "A pack of 10 A4 red envelopes", Colour = "Red", Price = 19.99m },
            new() { Name = "Whiteboard marker", Description = "A pack of 10 red markers", Colour = "Red", Price = 49.99m }
        };

        var productDTOs = new List<ProductDTO>
        {
            new() { Name = "Envelopes", Description = "A pack of 10 A4 red envelopes", Colour = "Red", Price = 19.99m },
            new() { Name = "Whiteboard marker", Description = "A pack of 10 red markers", Colour = "Red", Price = 49.99m }
        };

        _mockProductReadRepository.Setup(repo => repo.GetByColourAsync(query.Colour))
                                  .ReturnsAsync(products);

        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(products))
                   .Returns(productDTOs);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count());
        _mockMapper.Verify(m => m.Map<IEnumerable<ProductDTO>>(products), Times.Once);
    }

    [Fact]
    public async Task Handle_NoProductsWithGivenColour_ReturnsEmptyList()
    {
        // Arrange
        var query = new GetProductsByColourQuery { Colour = "Green" };

        _mockProductReadRepository.Setup(repo => repo.GetByColourAsync(query.Colour))
                                  .ReturnsAsync(new List<Product>());

        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(It.IsAny<IEnumerable<Product>>()))
                   .Returns(new List<ProductDTO>());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result);
        _mockMapper.Verify(m => m.Map<IEnumerable<ProductDTO>>(It.IsAny<IEnumerable<Product>>()), Times.Once);
    }
}
