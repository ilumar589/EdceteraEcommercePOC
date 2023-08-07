using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(IDbContextFactory<CatalogContext> catalogContextFactory) : base(catalogContextFactory)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsWithSimilarName(string productName)
    {
        var productList = await GetAsync(p => p.Name.Contains(productName));
        return productList;
    }
}