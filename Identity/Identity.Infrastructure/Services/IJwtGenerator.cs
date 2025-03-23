public interface IJwtGenerator
{
    Task<string> GenerateToken(User user, IEnumerable<string> roles);
}