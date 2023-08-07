using System.Reflection;
using Catalog.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        // DI registration for FluentValidation classes
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // DI registration for MediatR
        serviceCollection.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        return serviceCollection;
    }
}