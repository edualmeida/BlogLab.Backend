using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models.Users;

public class Role : IdentityRole<Guid>
{
    public Role(){}
    public Role(string roleName) : base(roleName)
    {
    }
}