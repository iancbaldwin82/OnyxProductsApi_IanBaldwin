using Domain;

namespace Application.Interfaces;

public interface IProductWriteRepository
{
    Task AddAsync(Product product);
}
