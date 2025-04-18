using System.Reflection;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleCatalog.Domain;
public static class DomainConfiguration
{
    public static IServiceCollection AddArticleCatalogDomain(
        this IServiceCollection services)
        => services
            .AddCommonDomain(Assembly.GetExecutingAssembly());
}