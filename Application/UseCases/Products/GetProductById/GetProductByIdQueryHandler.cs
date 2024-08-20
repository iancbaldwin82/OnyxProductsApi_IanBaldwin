using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Domain;

namespace Application.UseCases.Products.GetProductById
{
    public class GetProductByIdQueryHandler(IProductReadRepository repository, 
                                            IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDTO?>
    {
        public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(request.Id);
            return mapper.Map<ProductDTO?>(product);
        }
    }
}
