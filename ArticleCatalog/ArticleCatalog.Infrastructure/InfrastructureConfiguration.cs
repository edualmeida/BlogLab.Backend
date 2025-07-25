﻿using ArticleCatalog.Application.Services;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ArticleCatalog.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddArticleCatalogInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonInfrastructure(Assembly.GetExecutingAssembly(), configuration)
            .ConfigureElasticsearch(configuration)
            .ConfigureCaching(configuration)
            .AddDatabaseStorage<ArticleCatalogDbContext>(configuration, Assembly.GetExecutingAssembly())
            .AddHttpClients(configuration);

    private static IServiceCollection ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            //.Configure<ArticleCacheOptions>(configuration.GetSection("RedisConfiguration"))
            //.AddSingleton<IRedisConnectionBuilder, RedisConnectionBuilder>()
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
        services.AddHttpClient<AuthorsHttpService>(httpClient =>
        {
            httpClient.BaseAddress = new($"https+http://{InfrastructureConstants.IdentityApiName}");
            httpClient.ConfigureApiKey(configuration);
        })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IAuthorsHttpService, AuthorsHttpService>();

        services.AddHttpClient<BookmarksHttpService>((sp, httpClient) =>
        {
            httpClient.BaseAddress = new($"https+http://{InfrastructureConstants.BookmarksApiName}");
        })
            .AddHttpMessageHandler<FowardAuthorizationHeaderHandler>()
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IBookmarksHttpService, BookmarksHttpService>();

        return services;
    }
}