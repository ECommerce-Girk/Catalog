using Catalog.Core.Products;

namespace Catalog.Infra.Database.Repositories;

public class ProductRepository(CatalogDbContext _dbContext): IProductRepository
{
    public async Task<Product?> GetById(Guid id, CancellationToken ct)
    {
        return await _dbContext.Products.FindAsync(id, ct);
    }
}