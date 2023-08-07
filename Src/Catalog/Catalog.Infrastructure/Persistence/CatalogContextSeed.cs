using System.Collections.Immutable;
using Catalog.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence;

public static class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext, ILogger<CatalogContext> logger)
    {
        if (!catalogContext.Products.Any())
        {
            await catalogContext.Products.AddRangeAsync(GetPreconfiguredProducts());
            await catalogContext.SaveChangesAsync();
            logger.LogInformation("Seeded database associated with context {DbContextName}", nameof(CatalogContext));
        }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new()
            {
                Name = "Demo product 1",
                Description = "Demo product 1 description",
                Type = ProductType.Course
            },
            new()
            {
                Name = "Demo product 2",
                Description = "Demo product 2 description",
                Type = ProductType.Webinar
            }
        }.ToImmutableList();
    }
}