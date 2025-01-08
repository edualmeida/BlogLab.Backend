using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static ArticleCatalogSettings GetArticleCatalogSettings(
        this IConfiguration configuration)
    {
        var articleCatalogSettings = configuration.GetSection(nameof(ArticleCatalogSettings));
        var authorsAPIClientSettings = articleCatalogSettings.GetSection(nameof(AuthorsAPIClientSettings));

        return new ArticleCatalogSettings(
            new(authorsAPIClientSettings.GetValue<string>(nameof(AuthorsAPIClientSettings.BaseUrl))!)
        );
    }
}