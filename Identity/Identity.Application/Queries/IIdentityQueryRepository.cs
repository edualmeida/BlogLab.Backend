public interface IIdentityQueryRepository : IQueryRepository<User>
{
    Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default);
    Task<UserResponse> GetById(Guid id, CancellationToken cancellationToken = default);
}