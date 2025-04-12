namespace ArticleCatalog.Application.Services;
public interface IBookmarksHttpService
{
    public Task<List<UserBookmarkResponse>> GetUserBookmarks(Guid userId, CancellationToken cancellationToken = default);
}
