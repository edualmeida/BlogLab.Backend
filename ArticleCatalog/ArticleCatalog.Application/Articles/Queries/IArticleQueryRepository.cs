public interface IArticleQueryRepository : IQueryRepository<Article>
{
    Task<ArticleResponse> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<GetAllResult> GetAll(
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default);
}