using System.Reflection;
using ArticleCatalog.Application;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleCatalog.Web;
public static class WebConfiguration
{
    public static IServiceCollection AddArticleCatalogWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(ArticlesApplicationConfiguration), Assembly.GetExecutingAssembly());
}