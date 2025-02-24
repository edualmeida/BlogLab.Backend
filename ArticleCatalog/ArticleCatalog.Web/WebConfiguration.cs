using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class WebConfiguration
{
    public static IServiceCollection AddArticleCatalogWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(ArticlesApplicationConfiguration), Assembly.GetExecutingAssembly());
}