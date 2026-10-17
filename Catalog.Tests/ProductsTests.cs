using System.Net;
using System.Net.Http.Json;
using Catalog.Core.Products;

namespace Catalog.Tests;

public class ProductsTests(CustomWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task CreateProduct_ReturnsCreatedProduct()
    {
        // Arrange
        var createProductRequest = new CreateProductModel 
        { 
            Name = "Test Product",
            Description = "This is a test product.",
            Price = 19.99m,
            StockQuantity = 100
        };

        // Act
        var response = await Client.PostAsJsonAsync("/Products", createProductRequest);
        var responseModel = await response.Content.ReadFromJsonAsync<Product>();

        // Assert response
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(responseModel);
        Assert.Equal(createProductRequest.Name, responseModel.Name);

        // Assert database state
        var ctx = GetDbContext();
        var ProductInDb = await ctx.Products.FindAsync(responseModel.Id);
        Assert.NotNull(ProductInDb);
        Assert.Equal(createProductRequest.Name, ProductInDb.Name);
    }
}