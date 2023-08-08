using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Models;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContextFactory<CatalogContext>(
            options =>
                options.UseSqlServer("Server=localhost,5434;Database=CatalogDb;User Id=sa;Password=SwN12345678;TrustServerCertificate=True"));
        
        serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();

        serviceCollection.Configure<IdentitySettings>(c => configuration.GetSection("KeyClockSettings"));
        
        return serviceCollection;
    }
}