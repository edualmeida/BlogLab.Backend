using Comments.Domain.Models.Comments;
using Comments.Domain.Repositories;
using Comments.Infrastructure.Repositories.Configuration;
using Common.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Comments.Infrastructure.Repositories;
internal class ElasticCommentRepository : ElasticsearchRepository, IElasticCommentRepository
{
    private readonly ILogger<ElasticCommentRepository> _logger;
    private readonly ElasticsearchOptions _configuration;

    public ElasticCommentRepository(
        ILogger<ElasticCommentRepository> logger,
        IOptions<ElasticsearchOptions> options
        ) : base(options.Value.NodeUri, options.Value.ApiKey)
    {
        _logger = logger;
        _configuration = options.Value;
    }

    public async Task<bool> CreateCommentAsync(ElasticComment comment)
    {
        var response = await CreateAsync(_configuration.IndexName, comment);

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
}
