public class ArticleCatalogAPIClientSettings(string baseUrl)
{
    public string BaseUrl { get; set; } = baseUrl;
}

public class BookmarksSettings(ArticleCatalogAPIClientSettings articleCatalogAPIClientSettings)
{
    public ArticleCatalogAPIClientSettings BikeCatalogAPIClientSettings { get; set; } 
        = articleCatalogAPIClientSettings;
}