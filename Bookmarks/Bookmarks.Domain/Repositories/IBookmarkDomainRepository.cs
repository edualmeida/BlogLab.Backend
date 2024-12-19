public interface IBookmarkDomainRepository : IDomainRepository<Bookmark>
{
    Task<Bookmark> Find(Guid id, CancellationToken cancellationToken = default);
    Task Delete(Guid articleId, CancellationToken cancellationToken = default);
}