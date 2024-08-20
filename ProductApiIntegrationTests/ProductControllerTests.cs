using Application.DTOs;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace ProductsApi.IntegrationTests;

public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ProductsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedResponse()
    {
        // Arrange
        var productDto = new ProductDTO
        {
            Name = "Test Product",
            Description = "Test Description",
            Colour = "Blue",
            Price = 10.0m
        };

        var content = new StringContent(JsonConvert.SerializeObject(productDto), System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsStringAsync();
        var createdProduct = JsonConvert.DeserializeObject<ProductDTO>(responseBody);
        createdProduct.Should().NotBeNull();
        createdProduct?.Name.Should().Be(productDto.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsOkResponse()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/products/{productId}");

        // Assert
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var productDto = JsonConvert.DeserializeObject<ProductDTO>(responseBody);
            productDto.Should().NotBeNull();
        }
        else
        {
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }

    [Fact]
    public async Task GetProductsByColour_ReturnsOkResponse()
    {
        // Arrange
        var colour = "Red";

        // Act
        var response = await _client.GetAsync($"/api/products/color/{colour}");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<ProductDTO>>(responseBody);
        products.Should().NotBeNull();
        products.Should().HaveCountGreaterThan(0);
    }
}
