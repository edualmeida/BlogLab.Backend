public class AuthorsAPIClientSettings(string baseUrl)
{
    public string BaseUrl { get; set; } = baseUrl;
}

public class ArticleCatalogSettings(AuthorsAPIClientSettings authorsAPIClientSettings)
{
    public AuthorsAPIClientSettings AuthorsAPIClientSettings { get; set; }
        = authorsAPIClientSettings;
}