using Catalog.Infra.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Tests;

[Collection(nameof(IntegrationTestCollection))]
public abstract class TestBase
{
    protected CustomWebApplicationFactory Factory { get; }
    protected HttpClient Client { get; }

    protected TestBase(CustomWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
    }

    protected CatalogDbContext GetDbContext()
    {
        var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        return dbContext;
    }
}

[CollectionDefinition(nameof(IntegrationTestCollection))]
public sealed class IntegrationTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
};
