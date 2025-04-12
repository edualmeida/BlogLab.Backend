using System.Reflection;
using Common.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleCatalog.Application;
public static class ArticlesApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddArticleCatalogApplication(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonApplication(configuration, Assembly);
}