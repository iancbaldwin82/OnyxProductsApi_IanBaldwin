using Application.DTOs;
using MediatR;

namespace Application.UseCases.Products.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDTO?>
{
    public required Guid Id { get; set; }
}
