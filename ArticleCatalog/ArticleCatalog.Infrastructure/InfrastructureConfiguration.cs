using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddArticleCatalogInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDBStorage<BlogDbContext>(configuration, Assembly.GetExecutingAssembly())
            .AddTransient<IDbInitializer, BlogDbInitializer>();
}