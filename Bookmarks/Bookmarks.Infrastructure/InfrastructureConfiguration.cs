using System.Reflection;
using Bookmarks.Application.Services;
using Bookmarks.Infrastructure.HttpServices;
using Bookmarks.Infrastructure.Persistence;
using Common.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmarks.Infrastructure;
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

        return services
            .AddHttpClientSettings(bookmarksConfiguration)
            .AddHttpClient<ArticleCatalogHttpService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(bookmarksConfiguration.ArticleCatalogApiClientSettings.BaseUrl);
                httpClient.ConfigureApiKey(configuration);
            })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IArticleCatalogHttpService, ArticleCatalogHttpService>()
            .Services;
    }
    
    private static IServiceCollection AddHttpClientSettings(
        this IServiceCollection services,
        BookmarksSettings settings)
    {
        services.AddSingleton(settings.ArticleCatalogApiClientSettings);
        return services;
    }
}