using Identity.Application.Queries.Common;
using Identity.Domain.Models.Users;

namespace Identity.Application.Queries;
public interface IIdentityQueryRepository : IQueryRepository<User>
{
    Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default);
    Task<UserResponse> GetById(Guid id, CancellationToken cancellationToken = default);
}