namespace Common.Infrastructure.Repositories.Configuration;
public interface IElasticsearchOptions
{
    public string NodeUri { get; set; }
    public string ApiKey { get; set; }
    public string IndexName { get; set; }
}
