using Catalog.Core.Products;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Database.Repositories;

public class ProductRepository(CatalogDbContext _dbContext): IProductRepository
{
    public async Task<Product?> GetByName(string name, CancellationToken ct)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name, ct);
    }
}