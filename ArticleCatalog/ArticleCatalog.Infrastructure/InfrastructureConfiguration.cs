using ArticleCatalog.Application.Services;
using ArticleCatalog.Domain.Repositories;
using ArticleCatalog.Infrastructure.Extensions;
using ArticleCatalog.Infrastructure.HttpServices;
using ArticleCatalog.Infrastructure.Persistence;
using ArticleCatalog.Infrastructure.Repositories;
using ArticleCatalog.Infrastructure.Repositories.Configuration;
using Common.Domain;
using Common.Infrastructure;
using Common.Infrastructure.Authentication.HttpMessageHandlers;
using Common.Infrastructure.Repositories;
using Common.Infrastructure.Repositories.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ArticleCatalog.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddArticleCatalogInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonInfrastructure(configuration)
            .ConfigureElasticsearch(configuration)
            .ConfigureCaching(configuration)
            .AddDabaseStorage<ArticleCatalogDbContext>(configuration, Assembly.GetExecutingAssembly())
            .AddTransient<IDbInitializer, ArticleCatalogDbInitializer>()
            .AddHttpClients(configuration);

    private static IServiceCollection ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<ArticleCacheOptions>(configuration.GetSection("RedisConfiguration"))
            .AddSingleton<IRedisConnectionBuilder, RedisConnectionBuilder>()
            .AddScoped<ICacheRepository, RedisRepository>();
    }

    private static IServiceCollection ConfigureElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<ElasticsArticleOptions>(configuration.GetSection("ElasticsearchConfiguration"))
            .AddScoped<IElasticArticleRepository, ElasticArticleRepository>();
    }

    private static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var httpClientSettings = configuration.GetArticleCatalogSettings();
        services.AddHttpClient<AuthorsHttpService>(httpClient =>
        {
            httpClient.BaseAddress = new("https+http://identity");
            httpClient.ConfigureApiKey(configuration);
        })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IAuthorsHttpService, AuthorsHttpService>();

        services.AddHttpClient<BookmarksHttpService>((sp, httpClient) =>
        {
            httpClient.BaseAddress = new("https+http://bookmarks");
        })
            .AddHttpMessageHandler<FowardAuthorizationHeaderHandler>()
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IBookmarksHttpService, BookmarksHttpService>();

        return services;
    }
}