public interface IArticleCatalogHttpService
{
    public Task<List<ArticleResponse>> GetArticlesByIds(IEnumerable<string> ids);
}