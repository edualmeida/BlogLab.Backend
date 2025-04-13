using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Common.Infrastructure.Authentication.HttpMessageHandlers;
public sealed class FowardAuthorizationHeaderHandler(IHttpContextAccessor accessor) : DelegatingHandler
{
    private const string headerKey = "Authorization";
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (accessor.HttpContext?.Request.Headers != null &&
            accessor.HttpContext.Request.Headers.TryGetValue(headerKey, out var stringValues) &&
                AuthenticationHeaderValue.TryParse(stringValues, out var authHeaderValue))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(authHeaderValue.Scheme, authHeaderValue.Parameter);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
