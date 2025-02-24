using System.Net.Http.Json;

public sealed class ArticleCatalogHttpService(
    HttpClient client,
    ArticleCatalogApiClientSettings settings
    ) : IArticleCatalogHttpService
{
    public async Task<List<ArticleResponse>> GetArticlesByIds(IEnumerable<string> ids)
    {
        //var idsUri = string.Join("&", ids);

        //return await client.GetFromJsonAsync<List<ArticleResponse>>(idsUri) ?? [];
        return await client.GetFromJsonAsync<List<ArticleResponse>>(settings.GetArticlesByIdsPath) ?? [];
    }
}