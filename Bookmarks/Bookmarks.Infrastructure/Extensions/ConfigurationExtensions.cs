using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static BookmarksSettings GetBookmarksSettings(
        this IConfiguration configuration)
    {
        var bookmarkManagementSettings = configuration.GetSection(nameof(BookmarksSettings));
        var articleCatalogAPIClientSettings = bookmarkManagementSettings.GetSection(nameof(ArticleCatalogAPIClientSettings));

        return new BookmarksSettings(
                new(articleCatalogAPIClientSettings.GetValue<string>(nameof(ArticleCatalogAPIClientSettings.BaseUrl)))
            );
    }
}