using ArticleCatalog.Application.Contracts.Bookmarks;
using ArticleCatalog.Application.Services;
using Common.Infrastructure.Extensions;

namespace ArticleCatalog.Infrastructure.HttpServices;
public sealed class BookmarksHttpService(HttpClient client) : IBookmarksHttpService
{
    public async Task<List<UserBookmarkResponse>> GetUserBookmarks(CancellationToken cancellationToken = default)
        => await client.Get<List<UserBookmarkResponse>>($"/api/bookmarks", cancellationToken) ?? [];
}