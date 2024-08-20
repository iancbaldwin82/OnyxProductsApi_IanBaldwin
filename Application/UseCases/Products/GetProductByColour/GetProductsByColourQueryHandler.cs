using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Products.GetProductsByColour;

public class GetProductsByColourQueryHandler(IProductReadRepository repository,
                                            IMapper mapper) : IRequestHandler<GetProductsByColourQuery, IEnumerable<ProductDTO>>
{
    public async Task<IEnumerable<ProductDTO>> Handle(GetProductsByColourQuery request, CancellationToken cancellationToken)
    {
        var products = await repository.GetByColourAsync(request.Colour);
        return mapper.Map<IEnumerable<ProductDTO>>(products);
    }
}