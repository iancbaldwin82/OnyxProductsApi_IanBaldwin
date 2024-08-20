using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.UseCases.Products.AddProduct;

public class AddProductCommandHandler(IProductWriteRepository productRepository) : IRequestHandler<AddProductCommand, Unit>
{
    public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {        
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Colour = request.Colour,
            Price = request.Price
        };
        
        await productRepository.AddAsync(product);
        
        return Unit.Value;
    }
}
