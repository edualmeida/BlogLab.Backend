using System.Reflection;
using ArticleCatalog.Application.Services;
using ArticleCatalog.Domain.Repositories;
using ArticleCatalog.Infrastructure.Extensions;
using ArticleCatalog.Infrastructure.HttpServices;
using ArticleCatalog.Infrastructure.Persistence;
using ArticleCatalog.Infrastructure.Repositories;
using Common.Infrastructure;
using Common.Infrastructure.Authentication.HttpMessageHandlers;
using Common.Infrastructure.Repositories.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleCatalog.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddArticleCatalogInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddScoped(x => new ElasticsearchConfiguration(
                "http://localhost:9200", 
                "TXk0WHk1WUJNZnNKT3l6dmhzY1c6OU9VRDYtdmVZTlBTUXRVTTR6QVBZQQ=="))

            .AddScoped<IElasticArticleRepository, ElasticArticleRepository>()
            .AddDabaseStorage<ArticleCatalogDbContext>(configuration, Assembly.GetExecutingAssembly())
            .AddTransient<IDbInitializer, ArticleCatalogDbInitializer>()
            .AddHttpClients(configuration);

    private static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var httpClientSettings = configuration.GetArticleCatalogSettings();
        services.AddHttpClient<AuthorsHttpService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings.AuthorsApiClientSettings.BaseUrl);
                httpClient.ConfigureApiKey(configuration);
            })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IAuthorsHttpService, AuthorsHttpService>();
        
        services.AddHttpClient<BookmarksHttpService>((sp, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings.BookmarksApiClientSettings.BaseUrl);
            })
            .AddHttpMessageHandler<FowardAuthorizationHeaderHandler>()
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IBookmarksHttpService, BookmarksHttpService>();
        
        return services;
    }
}