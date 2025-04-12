using System.Security.Claims;
using Common.Application.Contracts;
using Common.Web.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Common.Web.Services;
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : 
    ICurrentUserService
{
    public Guid? GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.AuthenticationType != JwtBearerDefaults.AuthenticationScheme)
        {
            return null;
        }

        var nameIdentifier = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(nameIdentifier, out Guid userId))
        {
            throw new UserIdGuidIsInvalidException(nameIdentifier);
        }

        return userId;
    }

    public Guid GetRequiredUserId()
    {
        var user = (httpContextAccessor.HttpContext?.User) ?? 
            throw new InvalidOperationException("This request does not have an authenticated user.");
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return new Guid(userId);
    }
}