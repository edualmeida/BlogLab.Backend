public interface IIdentityQueryRepository : IQueryRepository<User>
{
    Task<List<UserResponse>> GetAll(CancellationToken cancellationToken = default);
}