using Base;

namespace Catalog.Core.Products;

public class CreateProduct
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateProduct(IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Product> Execute(CreateProductModel request, CancellationToken ct)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };
        
        _unitOfWork.Add(product);
        await _unitOfWork.SaveChangesAsync(ct);
        
        return product;
    }
}