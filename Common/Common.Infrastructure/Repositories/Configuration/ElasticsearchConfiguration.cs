namespace Common.Infrastructure.Repositories.Configuration;
public sealed class ElasticsearchConfiguration(
    string nodeUri,
    string apiKey)
{
    public string NodeUri => nodeUri;
    public string ApiKey => apiKey;
}
