using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddBookmarksInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDBStorage<BookmarksDbContext>(
                configuration,
                Assembly.GetExecutingAssembly())
            .AddHttpClients(configuration);

    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddHttpClient<ArticleCatalogHttpService>(httpClient =>
            {
                var httpClientSettings = configuration.GetBookmarksSettings();
                httpClient.BaseAddress = new Uri(httpClientSettings.ArticleCatalogAPIClientSettings.BaseUrl);
            })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IArticleCatalogHttpService, ArticleCatalogHttpService>()
            .Services;
}