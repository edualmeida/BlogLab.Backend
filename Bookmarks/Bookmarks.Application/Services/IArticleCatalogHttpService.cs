using Bookmarks.Application.Services.Contracts.Articles;

namespace Bookmarks.Application.Services;
public interface IArticleCatalogHttpService
{
    public Task<List<ArticleResponse>> GetArticlesByIds(IEnumerable<string> ids);
}