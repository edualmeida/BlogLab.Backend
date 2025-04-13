namespace ArticleCatalog.Application.Services;
public interface IBookmarksHttpService
{
    public Task<List<UserBookmarkResponse>> GetUserBookmarks(CancellationToken cancellationToken = default);
}
