using Domain;

namespace Application.Interfaces;

public interface IProductReadRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Product?>> GetByColourAsync(string colour);        
}
