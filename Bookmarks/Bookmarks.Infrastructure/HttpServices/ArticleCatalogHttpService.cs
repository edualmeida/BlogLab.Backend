using System.Net;
using System.Net.Http.Json;
using Bookmarks.Application.Services;
using Bookmarks.Application.Services.Contracts.Articles;
using Bookmarks.Infrastructure.HttpServices.Exceptions;

namespace Bookmarks.Infrastructure.HttpServices;
public sealed class ArticleCatalogHttpService(
    HttpClient client,
    ArticleCatalogApiClientSettings settings
    ) : IArticleCatalogHttpService
{
    public async Task<List<HttpArticleResponse>> GetArticlesByIds(IEnumerable<Guid> ids)
    {
        var response = await client.PostAsJsonAsync(
            settings.GetArticlesByIdsPath, 
            new { articleIds = ids});

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new ArticlesNotFoundException();
        }
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<HttpArticleResponse>>();
    }
}