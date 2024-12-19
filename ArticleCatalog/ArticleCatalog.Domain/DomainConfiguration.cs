using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class DomainConfiguration
{
    public static IServiceCollection AddArticleCatalogDomain(
        this IServiceCollection services)
        => services
            .AddCommonDomain(Assembly.GetExecutingAssembly());
}