using ArticleCatalog.Domain.Models.Articles;
using Common.Domain;

namespace ArticleCatalog.Domain.Repositories;
public interface IArticlesDomainRepository : IDomainRepository<Article>
{
    Task<Article?> Find(Guid id, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}