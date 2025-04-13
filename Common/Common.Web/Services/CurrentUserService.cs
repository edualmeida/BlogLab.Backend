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
        
        var identities = user?.Identities
            .Where(x=>
            x.AuthenticationType == JwtBearerDefaults.AuthenticationScheme ||
            x.AuthenticationType == "AuthenticationTypes.Federation")?.ToList() ?? [];
        
        if (identities.Count == 0)
        {
            return null;
        }

        var nameIdentifierClaim = identities[0].Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(nameIdentifierClaim?.Value, out Guid userId))
        {
            throw new UserIdGuidIsInvalidException(nameIdentifierClaim?.Value ?? "null or empty");
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