public interface IUserDomainRepository : IDomainRepository<User>
{
    Task<User?> Find(Guid id, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}