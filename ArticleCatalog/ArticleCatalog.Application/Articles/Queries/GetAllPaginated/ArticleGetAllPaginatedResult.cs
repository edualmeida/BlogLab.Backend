using ArticleCatalog.Application.Articles.Queries.Common;

namespace ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
public class ArticleGetAllPaginatedResult
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<ArticleQueryResponse> Articles { get; set; } = [];
}