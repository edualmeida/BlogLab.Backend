namespace ArticleCatalog.Application.Settings;

public class ArticleCatalogSettings(
    AuthorsApiClientSettings authorsApiClientSettings,
    BookmarksApiClientSettings bookmarksApiClientSettings)
{
    public AuthorsApiClientSettings AuthorsApiClientSettings { get; }
        = authorsApiClientSettings;
    
    public BookmarksApiClientSettings BookmarksApiClientSettings { get; }
        = bookmarksApiClientSettings;
}