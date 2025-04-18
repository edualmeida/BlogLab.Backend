using AutoMapper;
using Identity.Application.Queries;
using Identity.Application.Queries.Common;
using Identity.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;
internal class IdentityRepository(
    UserManager<User> userManager, 
    IMapper mapper) : IIdentityQueryRepository
{
    public async Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default)
        => (await mapper
            .ProjectTo<UserResponse>(userManager.Users)
            .ToListAsync(cancellationToken));

    public async Task<UserResponse> GetById(Guid id, CancellationToken cancellationToken = default)
        => mapper.Map<UserResponse>(await userManager.FindByIdAsync(id.ToString()));
}