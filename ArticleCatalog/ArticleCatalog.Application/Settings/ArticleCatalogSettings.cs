namespace ArticleCatalog.Application.Settings;

public class ArticleCatalogSettings(AuthorsApiClientSettings authorsApiClientSettings)
{
    public AuthorsApiClientSettings AuthorsApiClientSettings { get; set; }
        = authorsApiClientSettings;
}