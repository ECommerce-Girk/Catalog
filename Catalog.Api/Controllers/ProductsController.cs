using Catalog.Core.Products;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class ProductsController : ControllerBase
{
    [HttpPost("/products")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductModel request,
        [FromServices] CreateProduct cmd,
        CancellationToken ct)
    {
        var record = await cmd.Execute(request, ct);
        return Created("/products/" + record.Id, record);
    }
    
    [HttpGet("/products/{id:guid}")]
    public async Task<IActionResult> GetProduct(
        Guid id,
        [FromServices] IProductRepository repository,
        CancellationToken ct)
    {
        var product = await repository.GetById(id, ct);
        
        if (product is null)
            return NotFound();
            
        return Ok(product);
    }
}