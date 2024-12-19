public interface IBookmarkQueryRepository : IQueryRepository<Bookmark>
{
    Task<List<BookmarkResponse>> GetAll(CancellationToken cancellationToken = default);
}