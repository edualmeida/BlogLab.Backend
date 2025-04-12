public class ArticleCatalogApiClientSettings(
    string baseUrl,
    string getArticlesByIdsPath)
{
    public string BaseUrl { get; private set; } = baseUrl;
    public string GetArticlesByIdsPath { get; private set; } = getArticlesByIdsPath;
}

public class BookmarksSettings(ArticleCatalogApiClientSettings articleCatalogApiClientSettings)
{
    public ArticleCatalogApiClientSettings ArticleCatalogApiClientSettings { get; private set; } 
        = articleCatalogApiClientSettings;
}