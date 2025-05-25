using Comments.Domain.Models.Comments;
using Comments.Domain.Repositories;
using Comments.Infrastructure.Repositories.Configuration;
using Common.Infrastructure.Repositories;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Sockets;

namespace Comments.Infrastructure.Repositories;
internal class ElasticCommentRepository : ElasticsearchRepository, IElasticCommentRepository
{
    private readonly ILogger<ElasticCommentRepository> _logger;
    private readonly CommentsElasticsearchOptions _configuration;

    public ElasticCommentRepository(
        ILogger<ElasticCommentRepository> logger,
        IOptions<CommentsElasticsearchOptions> options
        ) : base(options.Value)
    {
        _logger = logger;
        _configuration = options.Value;
    }

    public async Task<bool> CreateCommentAsync(ElasticComment comment)
    {
        var response = await CreateWithPipelineAsync(_configuration.IndexName, comment, "enrich-comments-with-article");

        if (!response.IsValidResponse)
        {
            _logger.LogError(
                "Failed to index document with ID {DocumentId}. Error: {Error}. Debug: {DebugInfo}",
                response.Id,
                response.ElasticsearchServerError?.ToString() ?? "No server error details",
                response.DebugInformation
            );

            return false;
        }

        _logger.LogInformation("Index document with ID {ResponseId} succeeded.", response.Id);

        return true;
    }

    /// <summary>
    /// Dictionary<string, long> where:
    /// Each key is a category name(like "Design Patterns"), 
    /// Each value is the number of comments made on articles in that category.
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<string, long>> GetCommentCountsByCategoryAsync()
    {
        var response = await _elasticClient.SearchAsync<ElasticComment>(s => s
            .Indices("comments")
            .Size(0) // not to return any actual comment documents 
            .Aggregations(agg => agg
                .Add("by_category", a => a
                    .Terms(t => t
                        .Field("article.category.keyword")
                        .Size(10)
                    )
                )
            )
        );

        var result = new Dictionary<string, long>();
        var states = response.Aggregations!.GetStringTerms("states");

        foreach (var item in states!.Buckets)
        {
            result[item.Key.ToString()] = item.DocCount;
        }

        return result;
    }
}
