public interface IAuthorsHttpService
{
    public Task<List<AuthorResponse>> GetAll(CancellationToken cancellationToken = default);
}
