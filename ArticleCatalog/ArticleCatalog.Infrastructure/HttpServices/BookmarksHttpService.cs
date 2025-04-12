using System.Net.Http.Json;
using ArticleCatalog.Application.Services;

namespace ArticleCatalog.Infrastructure.HttpServices;
public sealed class BookmarksHttpService(HttpClient client) : IBookmarksHttpService
{
    public async Task<List<UserBookmarkResponse>> GetUserBookmarks(Guid userId, CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<List<UserBookmarkResponse>>($"/api/bookmark?userid="+userId, cancellationToken) ?? [];
}