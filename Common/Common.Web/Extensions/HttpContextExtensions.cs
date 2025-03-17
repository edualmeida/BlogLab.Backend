using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common.Web.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        return httpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)?
            .ToUpperInvariant();
    }
    
    public static Guid GetRequiredUserId(this HttpContext httpContext)
    {
        var userId = httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return new Guid(userId.ToUpperInvariant());
    }
}