using System.Net.Http.Json;

public sealed class ArticleCatalogHttpService : IArticleCatalogHttpService
{
    private readonly HttpClient client;

    public ArticleCatalogHttpService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<ArticleResponse>> GetBikesByIds(IEnumerable<string> ids)
    {
        //var idsUri = string.Join("&", ids);

        //return await client.GetFromJsonAsync<List<ArticleResponse>>(idsUri) ?? [];
        return await client.GetFromJsonAsync<List<ArticleResponse>>("/api/articles") ?? [];
    }
}