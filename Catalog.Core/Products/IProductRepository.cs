namespace Catalog.Core.Products;

public interface IProductRepository
{
    Task<Product?> GetByName(string name, CancellationToken ct);
}