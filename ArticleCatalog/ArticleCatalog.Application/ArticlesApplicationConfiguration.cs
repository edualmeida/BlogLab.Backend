using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ArticlesApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddArticleCatalogApplication(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonApplication(configuration, Assembly);
}