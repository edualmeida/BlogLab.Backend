using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TValue?> Get<TValue>(this HttpClient client, string url, CancellationToken cancellationToken = default)
    {
        TValue? response = default;

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
        }
        
        
        return response;
    }
}