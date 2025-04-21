using ArticleCatalog.Application.Articles.Queries.Common;

namespace ArticleCatalog.Application.Articles.Queries.GetPaginated;
public class GetArticlesPaginatedResult
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<ArticleQueryResponse> Articles { get; set; } = [];
}