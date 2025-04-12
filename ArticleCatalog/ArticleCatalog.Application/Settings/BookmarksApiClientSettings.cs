namespace ArticleCatalog.Application.Settings;

public class BookmarksApiClientSettings(string baseUrl)
{
    public string BaseUrl { get; private set; } = baseUrl;
}