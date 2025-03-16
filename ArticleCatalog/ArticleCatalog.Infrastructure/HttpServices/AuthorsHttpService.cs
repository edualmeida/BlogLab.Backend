using System.Net.Http.Json;
using ArticleCatalog.Application.Services;

namespace ArticleCatalog.Infrastructure.HttpServices;

public sealed class AuthorsHttpService(HttpClient client) : IAuthorsHttpService
{
    public async Task<List<AuthorResponse>> GetAll(CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<List<AuthorResponse>>($"/api/identity", cancellationToken) ?? [];

    public async Task<AuthorResponse> GetById(Guid authorId, CancellationToken cancellationToken = default)
        => (await client.GetFromJsonAsync<AuthorResponse>($"/api/identity/" + authorId.ToString(), cancellationToken))!;
}