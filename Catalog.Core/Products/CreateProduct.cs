using Base;

namespace Catalog.Core.Products;

public class CreateProductModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }
}

public class CreateProduct(IUnitOfWork _uow)
{
    public async Task<Product> Execute(CreateProductModel request, CancellationToken ct)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };
        
        _uow.Add(product);
        await _uow.SaveChangesAsync(ct);
        
        return product;
    }
}