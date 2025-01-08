using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class IdentityRepository(
    UserManager<User> userManager, 
    IMapper mapper) : IIdentityQueryRepository
{
    public async Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default)
        => (await mapper
            .ProjectTo<UserResponse>(userManager.Users)
            .ToListAsync(cancellationToken));
}