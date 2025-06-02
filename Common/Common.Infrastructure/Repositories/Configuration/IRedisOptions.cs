namespace Common.Infrastructure.Repositories.Configuration;
public interface IRedisOptions
{
    string ConnectionString { get; set; }
}
