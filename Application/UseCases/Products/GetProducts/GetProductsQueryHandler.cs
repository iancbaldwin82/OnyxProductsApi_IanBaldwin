using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Products.GetProducts;

public class GetProductsQueryHandler(IProductReadRepository repository, 
                                     IMapper mapper) : IRequestHandler<GetProductsQuery, IEnumerable<ProductDTO>>
{
    public async Task<IEnumerable<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<ProductDTO>>(products);
    }
}
