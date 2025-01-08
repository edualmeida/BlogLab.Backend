using System.Net.Http.Json;

public sealed class AuthorsHttpService : IAuthorsHttpService
{
    private readonly HttpClient client;

    public AuthorsHttpService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<AuthorResponse>> GetAll(CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<List<AuthorResponse>>($"/api/identity", cancellationToken) ?? [];
}