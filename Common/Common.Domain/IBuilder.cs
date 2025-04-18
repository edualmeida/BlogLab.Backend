namespace Common.Domain;
public interface IBuilder<out TEntity>
    where TEntity : IAggregateRoot
{
    TEntity Build();
}