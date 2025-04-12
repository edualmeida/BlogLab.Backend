namespace ArticleCatalog.Application.Services;
public interface IAuthorsHttpService
{
    public Task<List<AuthorResponse>> GetAll(CancellationToken cancellationToken = default);
    public Task<AuthorResponse?> GetById(Guid authorId, CancellationToken cancellationToken = default);
}
