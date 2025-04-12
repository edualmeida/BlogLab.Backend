using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Application.Articles.Queries;
public interface IArticleQueryRepository : IQueryRepository<Article>
{
    Task<ArticleQueryResponse?> GetById(
        Guid id, 
        CancellationToken cancellationToken = default);
    Task<List<ArticleQueryResponse>> GetByIds(
        List<Guid> ids, 
        CancellationToken cancellationToken = default);
    Task<ArticleGetAllPaginatedResult> GetAll(
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default);
}