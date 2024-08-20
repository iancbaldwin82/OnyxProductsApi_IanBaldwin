using Application.DTOs;
using MediatR;

namespace Application.UseCases.Products.GetProductsByColour;

public class GetProductsByColourQuery : IRequest<IEnumerable<ProductDTO>>
{
    public required string Colour { get; set; }
}
