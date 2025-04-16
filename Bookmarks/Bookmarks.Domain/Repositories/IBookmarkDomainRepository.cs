using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Domain.Repositories;
public interface IBookmarkDomainRepository : IDomainRepository<Bookmark>
{
    Task<Bookmark?> Find(Guid id, CancellationToken cancellationToken = default);
    Task Delete(Guid bookmarkId, CancellationToken cancellationToken = default);
}