using Application.Interfaces;
using Domain;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ProductWriteRepository(ApplicationDbContext context) : IProductWriteRepository
    {
        public async Task AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }
    }
}
