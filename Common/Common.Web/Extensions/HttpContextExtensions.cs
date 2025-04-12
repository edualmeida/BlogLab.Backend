using System.Security.Claims;
using Common.Web.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Common.Web.Extensions;

public static class HttpContextExtensions
{
    public static Guid? GetUserId(this HttpContext httpContext)
    {
        if (httpContext?.User?.Identity?.AuthenticationType != JwtBearerDefaults.AuthenticationScheme)
        {
            return null;
        }

        var nameIdentifier =  httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(nameIdentifier, out Guid userId))
        {
            throw new UserIdGuidIsInvalidException(nameIdentifier);
        }

        return userId;
    }
    
    public static Guid GetRequiredUserId(this HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return new Guid(userId);
    }
}