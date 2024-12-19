public interface IArticleCatalogHttpService
{
    public Task<List<ArticleResponse>> GetBikesByIds(IEnumerable<string> ids);
}