using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Products.GetProductById;
using AutoMapper;
using Domain;
using Moq;

namespace ProductApiTests.GetProductByIdTests;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductReadRepository> _mockProductReadRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _mockProductReadRepository = new Mock<IProductReadRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetProductByIdQueryHandler(_mockProductReadRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsMappedProductDTO()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var query = new GetProductByIdQuery { Id = productId };

        var product = new Product
        {
            Id = productId,
            Name = "Notebook",
            Description = "A 100 page spiral-bound notebook",
            Colour = "Blue",
            Price = 5.99m
        };

        var productDTO = new ProductDTO
        {
            Name = "Notebook",
            Description = "A 100 page spiral-bound notebook",
            Colour = "Blue",
            Price = 5.99m
        };

        _mockProductReadRepository.Setup(repo => repo.GetByIdAsync(productId))
                                  .ReturnsAsync(product);

        _mockMapper.Setup(m => m.Map<ProductDTO?>(product))
                   .Returns(productDTO);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDTO.Name, result?.Name);
        Assert.Equal(productDTO.Description, result?.Description);
        Assert.Equal(productDTO.Colour, result?.Colour);
        Assert.Equal(productDTO.Price, result?.Price);
        _mockProductReadRepository.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.Map<ProductDTO?>(product), Times.Once);
    }

    [Fact]
    public async Task Handle_ProductDoesNotExist_ReturnsNull()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var query = new GetProductByIdQuery { Id = productId };

        _mockProductReadRepository.Setup(repo => repo.GetByIdAsync(productId))
                                  .ReturnsAsync((Product)null);

        _mockMapper.Setup(m => m.Map<ProductDTO?>(It.IsAny<Product>()))
                   .Returns((ProductDTO?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _mockProductReadRepository.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.Map<ProductDTO?>(It.IsAny<Product>()), Times.Once);
    }
}
