using MediatR;

namespace Application.UseCases.Products.AddProduct;

public class AddProductCommand : IRequest<Unit>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Colour { get; set; }
    public required decimal Price { get; set; }
}
