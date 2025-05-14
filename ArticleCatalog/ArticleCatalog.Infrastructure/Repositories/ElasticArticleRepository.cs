using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using Common.Infrastructure.Repositories;
using Common.Infrastructure.Repositories.Configuration;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
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

    public async Task<IReadOnlyCollection<Guid>> SearchArticlesAsync(string query)
    {
        var articles = await SearchAsync<ElasticArticle>(IndexName, s => s
            .Query(q => q
                .Bool(b => b
                    .Should(
                        sh => sh
                            .Match(m => m
                                .Field(f => f.Title)
                                .Query(query)
                                .Fuzziness("AUTO")
                            ),
                        sh => sh
                            .Match(m => m
                                .Field(f => f.Subtitle)
                                .Query(query)
                                .Fuzziness("AUTO")
                            ),
                        sh => sh
                            .MatchPhrase(m => m
                                .Field(f => f.Title)
                                .Query(query)
                                .Slop(2)
                            ),
                        sh => sh
                            .MatchPhrase(m => m
                                .Field(f => f.Subtitle)
                                .Query(query)
                                .Slop(2)
                            )
                    )
                    .MinimumShouldMatch(1)
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

        _logger.LogInformation("Index document with ID {ResponseId} succeeded.", response.Id);

        return true;
    }
}
