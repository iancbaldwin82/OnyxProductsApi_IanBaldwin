using Application.Interfaces;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductReadRepository(ApplicationDbContext context) : IProductReadRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product?>> GetByColourAsync(string colour)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.Colour.ToLower() == colour.ToLower())
            .ToListAsync();
    }
}
