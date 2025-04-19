using Common.Domain;

namespace Common.Application.Contracts;
public interface IQueryRepository<in TEntity>
    where TEntity : IAggregateRoot
{
}