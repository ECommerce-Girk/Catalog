using Base;

namespace Catalog.Core.Products;

public class Product : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }
}