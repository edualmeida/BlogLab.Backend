using System.Net.Http.Json;

namespace Common.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TValue?> Get<TValue>(this HttpClient client, string url, CancellationToken cancellationToken = default)
    {
        TValue? response;

        try
        {
            response = await client.GetFromJsonAsync<TValue>(url, cancellationToken);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
            throw;
        }
        
        
        return response;
    }
}