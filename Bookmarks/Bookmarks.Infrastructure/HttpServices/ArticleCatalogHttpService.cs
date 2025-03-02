using System.Net.Http.Json;
using Bookmarks.Application.Services;
using Bookmarks.Application.Services.Contracts.Articles;

namespace Bookmarks.Infrastructure.HttpServices;
public sealed class ArticleCatalogHttpService(
    HttpClient client,
    ArticleCatalogApiClientSettings settings
    ) : IArticleCatalogHttpService
{
    public async Task<List<ArticleResponse>> GetArticlesByIds(IEnumerable<string> ids)
    {
        var articleIds = string.Join("&", ids);
        var response = await client.PostAsJsonAsync(settings.GetArticlesByIdsPath, articleIds);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ArticleResponse>>();
    }
}