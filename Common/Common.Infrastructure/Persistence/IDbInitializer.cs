namespace Common.Infrastructure.Persistence;
public interface IDbInitializer
{
    public string Name { get; }
    Task Initialize(CancellationToken stoppingToken);
}