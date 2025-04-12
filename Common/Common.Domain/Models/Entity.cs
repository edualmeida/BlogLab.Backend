namespace Common.Domain.Models;
public abstract class Entity : IEntity
{
    public Guid Id { get; protected set; } = Guid.Empty;
    public DateTime CreatedOnUtc { get; protected set; } = DateTime.UtcNow;
    public bool Enabled { get; protected set; } = true;
}