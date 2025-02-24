using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static BookmarksSettings GetBookmarksSettings(
        this IConfiguration configuration)
    {
        var bookmarkManagementSettings = configuration.GetRequiredSection(nameof(BookmarksSettings));
        var articleCatalogApiClientSettings = bookmarkManagementSettings.GetRequiredSection(nameof(ArticleCatalogApiClientSettings));

        return new BookmarksSettings(new ArticleCatalogApiClientSettings(
            articleCatalogApiClientSettings.GetValue<string>(nameof(ArticleCatalogApiClientSettings.BaseUrl))!, 
            articleCatalogApiClientSettings.GetValue<string>(nameof(ArticleCatalogApiClientSettings.GetArticlesByIdsPath))!
            )
        );
    }
}