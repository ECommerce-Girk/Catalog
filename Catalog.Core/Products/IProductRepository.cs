namespace Catalog.Core.Products;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id, CancellationToken ct);
}