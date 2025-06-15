using System.Reflection;
using ArticleCatalog.Application;
using Common.Web;

namespace ArticleCatalog.Api;
public static class WebConfiguration
{
    public static IServiceCollection AddArticleCatalogWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(ArticlesApplicationConfiguration), Assembly.GetExecutingAssembly());
}