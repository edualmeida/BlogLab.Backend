using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            httpClient.BaseAddress = new Uri(httpClientSettings.AuthorsAPIClientSettings.BaseUrl);
        })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IAuthorsHttpService, AuthorsHttpService>()
            .Services;
}