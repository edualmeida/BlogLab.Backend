using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Articles.Queries.GetPaginated;
using ArticleCatalog.Domain.Models.Articles;
using Common.Application.Contracts;

namespace ArticleCatalog.Application.Articles.Queries;
public interface IArticlesQueryRepository : IQueryRepository<Article>
{
    Task<ArticleQueryResponse?> GetById(
        Guid id, 
        CancellationToken cancellationToken = default);

    Task<List<ArticleQueryResponse>> GetByIds(
        List<Guid> ids, 
        CancellationToken cancellationToken = default);

    Task<GetArticlesPaginatedResult> GetAll(
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default);
}