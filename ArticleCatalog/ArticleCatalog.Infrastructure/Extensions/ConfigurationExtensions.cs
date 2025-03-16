using ArticleCatalog.Application.Settings;
using Microsoft.Extensions.Configuration;

namespace ArticleCatalog.Infrastructure.Extensions;
public static class ConfigurationExtensions
{
    public static ArticleCatalogSettings GetArticleCatalogSettings(
        this IConfiguration configuration)
    {
        var articleCatalogSettings = configuration.GetSection(nameof(ArticleCatalogSettings));
        var authorsApiClientSettings = articleCatalogSettings.GetSection(nameof(AuthorsApiClientSettings));
        var bookmarksApiClientSettings = articleCatalogSettings.GetSection(nameof(BookmarksApiClientSettings));

        return new ArticleCatalogSettings(
            new AuthorsApiClientSettings(
                authorsApiClientSettings.GetValue<string>(nameof(AuthorsApiClientSettings.BaseUrl))!),
            new BookmarksApiClientSettings(
                bookmarksApiClientSettings.GetValue<string>(nameof(BookmarksApiClientSettings.BaseUrl))!)
        );
    }
}