using Catalog.Infrastructure.Persistence;
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
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test"));
        
        return serviceCollection;
    }
}