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
    
    [Fact]
    public async Task GetProduct_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var product = GivenExistingProductInDatabase();

        // Act
        var response = await Client.GetAsync($"/Products/{product.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseModel = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(responseModel);
        Assert.Equal(product.Name, responseModel.Name);
        Assert.Equal(product.Description, responseModel.Description);
        Assert.Equal(product.Price, responseModel.Price);
        Assert.Equal(product.StockQuantity, responseModel.StockQuantity);
    }
    
    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Act
        var response = await Client.GetAsync("/Products/00000000-0000-0000-0000-000000000000");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private Product GivenExistingProductInDatabase()
    {
        var existingProduct = new Product
        { 
            Name = "Existing Product", 
            Description = "This is an existing product.",
            Price = 19.99m,
            StockQuantity = 100
        };

        var ctx = GetDbContext();
        ctx.Products.Add(existingProduct);
        ctx.SaveChanges();
        return existingProduct;
    }
}