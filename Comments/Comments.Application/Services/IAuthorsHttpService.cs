using Comments.Application.Contracts.Authors;

namespace Comments.Application.Services;
public interface IAuthorsHttpService
{
    public Task<List<AuthorResponse>> GetAll(CancellationToken cancellationToken = default);
    public Task<AuthorResponse?> GetById(Guid authorId, CancellationToken cancellationToken = default);
}
