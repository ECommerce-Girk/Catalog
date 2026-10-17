using Catalog.Core.Products;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Database;

public partial class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}