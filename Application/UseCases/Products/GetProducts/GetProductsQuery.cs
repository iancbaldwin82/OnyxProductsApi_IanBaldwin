using Application.DTOs;
using MediatR;

namespace Application.UseCases.Products.GetProducts;

public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
{   
}
