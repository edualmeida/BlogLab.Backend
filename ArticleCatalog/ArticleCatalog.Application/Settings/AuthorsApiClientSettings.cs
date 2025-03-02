namespace ArticleCatalog.Application.Settings;

public class AuthorsApiClientSettings(string baseUrl)
{
    public string BaseUrl { get; private set; } = baseUrl;
}