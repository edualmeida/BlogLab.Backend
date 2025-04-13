using System.Net.Http.Headers;
using System.Reflection;
using ArticleCatalog.Application.Services;
using ArticleCatalog.Infrastructure.Extensions;
using ArticleCatalog.Infrastructure.HttpServices;
using ArticleCatalog.Infrastructure.Persistence;
using Common.Infrastructure;
using Microsoft.AspNetCore.Http;
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
                httpClient.ConfigureApiKey(configuration);

                var accessor = sp.GetRequiredService<IHttpContextAccessor>();

                if (accessor.HttpContext?.Request.Headers == null)
                    return;

                if (accessor.HttpContext.Request.Headers.TryGetValue(
                    "Authorization", out var authHeaderValue) &&
                        AuthenticationHeaderValue.TryParse(
                            authHeaderValue, out var auth))
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(auth.Scheme, auth.Parameter);
                }
                else
                {
                    // incase there is a value from a previous generation
                    if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    {
                        httpClient.DefaultRequestHeaders.Remove("Authorization");
                    }
                }
            })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IBookmarksHttpService, BookmarksHttpService>();
        
        return services;
    }
}