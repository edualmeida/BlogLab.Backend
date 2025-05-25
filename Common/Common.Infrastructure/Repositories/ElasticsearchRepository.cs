using Common.Infrastructure.Repositories.Configuration;
using Elastic.Clients.Elasticsearch;

namespace Common.Infrastructure.Repositories;
public abstract class ElasticsearchRepository
{
    protected readonly ElasticsearchClient _elasticClient;

    protected ElasticsearchRepository(IElasticsearchOptions options)
    {
        var settings = new ElasticsearchClientSettings(new Uri(options.NodeUri))
            .Authentication(new Elastic.Transport.Base64ApiKey(options.ApiKey));

        _elasticClient = new ElasticsearchClient(settings);
    }

    public async Task<IndexResponse> CreateAsync<T>(string index, T document) where T : class
    {
        var response = await _elasticClient.IndexAsync(document, idx => idx.Index(index));
        return response;
    }
    
    public async Task<IndexResponse> CreateWithPipelineAsync<T>(string index, T document, string pipeline) where T : class
    {
        var response = await _elasticClient
            .IndexAsync(document, idx => idx
                .Index(index)
                .Pipeline(pipeline));

        return response;
    }

    public async Task<T?> ReadAsync<T>(string index, string id) where T : class
    {
        var response = await _elasticClient.GetAsync<T>(id, g => g.Index(index));
        return response.Found ? response.Source : null;
    }

    public async Task<DeleteResponse> DeleteAsync<T>(string index, string id) where T : class
    {
        var response = await _elasticClient.DeleteAsync<T>(id, d => d.Index(index));
        return response;
    }
    public async Task<IReadOnlyCollection<T>> SearchAsync<T>(string index, Func<SearchRequestDescriptor<T>, SearchRequestDescriptor<T>> selector) where T : class
    {
        var response = await _elasticClient.SearchAsync<T>(s => selector(s.Index(index)));
        return response.Documents;
    }
}
