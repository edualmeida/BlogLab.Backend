using System.Reflection;
using ArticleCatalog.Infrastructure.Extensions;
using ArticleCatalog.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleCatalog.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddArticleCatalogInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDabaseStorage<ArticleCatalogDbContext>(configuration, Assembly.GetExecutingAssembly())
            .AddTransient<IDbInitializer, ArticleCatalogDbInitializer>()
            .AddHttpClients(configuration);

    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddHttpClient<AuthorsHttpService>(httpClient =>
        {
            var httpClientSettings = configuration.GetArticleCatalogSettings();
            httpClient.BaseAddress = new Uri(httpClientSettings.AuthorsApiClientSettings.BaseUrl);
            httpClient.ConfigureApiKey(configuration);
        })
        .ConfigureDefaultHttpClientHandler()
        .AddTypedClient<IAuthorsHttpService, AuthorsHttpService>()
        .Services;
}