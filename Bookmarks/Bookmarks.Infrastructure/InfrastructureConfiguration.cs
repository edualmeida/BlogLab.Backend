using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddBookmarksInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDabaseStorage<BookmarksDbContext>(
                configuration,
                Assembly.GetExecutingAssembly())
            .AddHttpClients(configuration);

    private static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var bookmarksConfiguration = configuration.GetBookmarksSettings();

        services.AddSingleton<ArticleCatalogApiClientSettings>(bookmarksConfiguration.ArticleCatalogApiClientSettings); // TODO: organize
        return services.AddHttpClient<ArticleCatalogHttpService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(bookmarksConfiguration.ArticleCatalogApiClientSettings.BaseUrl);
                httpClient.ConfigureApiKey(configuration);
            })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IArticleCatalogHttpService, ArticleCatalogHttpService>()
            .Services;
    }
}