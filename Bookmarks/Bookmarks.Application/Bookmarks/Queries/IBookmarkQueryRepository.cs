using Bookmarks.Application.Bookmarks.Queries.Common;
using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Application.Bookmarks.Queries;
public interface IBookmarkQueryRepository : IQueryRepository<Bookmark>
{
    Task<List<BookmarkQueryResponse>> GetByUserId(Guid userid, CancellationToken cancellationToken = default);
}