using ArticleCatalog.Application.Behaviors;
using Common.Application;
using Common.Application.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ArticleCatalog.Application;
public static class ArticlesApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddArticleCatalogApplication(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonApplication(configuration, Assembly, cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly);
                cfg.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddBehavior(typeof(GetArticlesCacheBehavior));
            });
}