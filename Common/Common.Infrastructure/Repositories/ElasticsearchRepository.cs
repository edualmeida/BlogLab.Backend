using Common.Infrastructure.Repositories.Configuration;
using Elastic.Clients.Elasticsearch;

namespace Common.Infrastructure.Repositories;
public abstract class ElasticsearchRepository
{
    private readonly ElasticsearchClient _client;

    protected ElasticsearchRepository(
        ElasticsearchConfiguration configuration)
    {
        var settings = new ElasticsearchClientSettings(new Uri(configuration.NodeUri))
            .Authentication(new Elastic.Transport.Base64ApiKey(configuration.ApiKey));

        _client = new ElasticsearchClient(settings);
    }

    public async Task<IndexResponse> CreateAsync<T>(string index, T document) where T : class
    {
        var response = await _client.IndexAsync(document, idx => idx.Index(index));
        return response;
    }

    public async Task<T?> ReadAsync<T>(string index, string id) where T : class
    {
        var response = await _client.GetAsync<T>(id, g => g.Index(index));
        return response.Found ? response.Source : null;
    }

    public async Task<DeleteResponse> DeleteAsync<T>(string index, string id) where T : class
    {
        var response = await _client.DeleteAsync<T>(id, d => d.Index(index));
        return response;
    }
    public async Task<IReadOnlyCollection<T>> SearchAsync<T>(string index, Func<SearchRequestDescriptor<T>, SearchRequestDescriptor<T>> selector) where T : class
    {
        var response = await _client.SearchAsync<T>(s => selector(s.Index(index)));
        return response.Documents;
    }
}
