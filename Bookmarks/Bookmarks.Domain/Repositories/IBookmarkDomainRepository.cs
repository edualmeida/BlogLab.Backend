using Bookmarks.Domain.Models.Bookmarks;
using Common.Domain;

namespace Bookmarks.Domain.Repositories;
public interface IBookmarkDomainRepository : IDomainRepository<Bookmark>
{
    Task<Bookmark?> Find(Guid id, CancellationToken cancellationToken = default);
    Task<Bookmark?> FindByArticleId(Guid userId, Guid articleId, CancellationToken cancellationToken = default);
    Task Delete(Guid bookmarkId, CancellationToken cancellationToken = default);
}