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
        var identities = GetClaimsIdentities();

        if (identities.Count == 0)
        {
            return null;
        }

        return GetUserIdFromClaimsIdentities(identities);
    }

    public Guid GetRequiredUserId()
    {
        var identities = GetClaimsIdentities();

        if (identities.Count == 0)
        {
            throw new NoValidAuthorizationIdentityFoundException("CurrentUserService->GetClaimsIdentities");
        }

        return GetUserIdFromClaimsIdentities(identities);
    }

    private List<ClaimsIdentity> GetClaimsIdentities()
    {
        var user = httpContextAccessor.HttpContext?.User;
        var identities = user?.Identities
            .Where(x =>
            x.AuthenticationType == JwtBearerDefaults.AuthenticationScheme ||
            x.AuthenticationType == "AuthenticationTypes.Federation")?.ToList() ?? [];

        return identities;
    }

    private static Guid GetUserIdFromClaimsIdentities(List<ClaimsIdentity> identities)
    {
        var nameIdentifierClaim = identities[0].Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(nameIdentifierClaim?.Value, out Guid userId))
        {
            throw new UserIdGuidIsInvalidException(nameIdentifierClaim?.Value ?? "null or empty");
        }

        return userId;
    }
}