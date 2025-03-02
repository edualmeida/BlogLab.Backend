using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Domain.Repositories;
public interface IArticleDomainRepository : IDomainRepository<Article>
{
    Task<Article?> Find(Guid id, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}