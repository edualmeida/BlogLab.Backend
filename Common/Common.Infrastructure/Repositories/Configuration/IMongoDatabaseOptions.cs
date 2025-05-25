namespace Common.Infrastructure.Repositories.Configuration;
public interface IMongoDatabaseOptions
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
