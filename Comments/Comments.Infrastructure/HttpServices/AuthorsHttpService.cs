using System.Net.Http.Json;
using Comments.Application.Contracts.Authors;
using Comments.Application.Services;
using Common.Infrastructure.Extensions;

namespace Comments.Infrastructure.HttpServices;

public sealed class AuthorsHttpService(HttpClient client) : IAuthorsHttpService
{
    public async Task<List<AuthorResponse>> GetAll(CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<List<AuthorResponse>>($"/api/identity", cancellationToken) ?? [];

    public Task<AuthorResponse?> GetById(Guid authorId, CancellationToken cancellationToken = default)
        => client.Get<AuthorResponse?>($"/api/identity/" + authorId, cancellationToken);
}