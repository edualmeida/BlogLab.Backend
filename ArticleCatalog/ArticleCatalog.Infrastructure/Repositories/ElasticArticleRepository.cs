using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using Common.Infrastructure.Repositories;
using Common.Infrastructure.Repositories.Configuration;
using Microsoft.Extensions.Logging;

namespace ArticleCatalog.Infrastructure.Repositories;
public class ElasticArticleRepository : ElasticsearchRepository, IElasticArticleRepository
{
    private const string IndexName = "articles";
    private readonly ILogger<ElasticArticleRepository> _logger;

    public ElasticArticleRepository(
        ILogger<ElasticArticleRepository> logger,
        ElasticsearchConfiguration configuration
        ) : base(configuration)
    {
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Guid>> SearchArticlesByNameAsync(string name)
    {
        var articles = await SearchAsync<Article>(IndexName, s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Title)
                    .Query(name)
                )
            )
        );

        return articles.Select(a => a.Id).ToList();
    }

    public async Task<bool> CreateArticleAsync(Article article)
    {
        var response = await CreateAsync(IndexName, article);

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

        _logger.LogInformation($"Index document with ID {response.Id} succeeded.");

        return true;
    }
}
