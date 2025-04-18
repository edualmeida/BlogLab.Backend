using Identity.Domain.Models.Users;

namespace Identity.Infrastructure.Services;
public interface IJwtGenerator
{
    Task<string> GenerateToken(User user, IEnumerable<string> roles);
}