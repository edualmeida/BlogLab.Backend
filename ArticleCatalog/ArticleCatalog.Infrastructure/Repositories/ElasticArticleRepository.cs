using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using ArticleCatalog.Infrastructure.Repositories.Configuration;
using Common.Infrastructure.Repositories;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArticleCatalog.Infrastructure.Repositories;
public class ElasticArticleRepository : ElasticsearchRepository, IElasticArticleRepository
{
    private readonly ILogger<ElasticArticleRepository> _logger;
    private readonly ElasticsArticleOptions _configuration;

    public ElasticArticleRepository(
        ILogger<ElasticArticleRepository> logger,
        IOptions<ElasticsArticleOptions> options
        ) : base(options.Value)
    {
        _logger = logger;
        _configuration = options.Value;
    }

    public async Task<IReadOnlyCollection<Guid>> SearchArticlesAsync(string query)
    {
        var articles = await SearchAsync<ElasticArticle>(_configuration.IndexName, s => s
            .Query(q => q
                .Bool(b => b
                    .Should(
                        sh => sh
                        .MultiMatch(mm => mm
                                .Fields(
                                    f=> f.Title!, 
                                    f => f.Subtitle!
                                )
                                .Query(query)
                                .Fuzziness("AUTO") // typo-tolerant and spelling mistakes
                            ),
                        sh => sh
                        .MultiMatch(mm => mm
                            .Fields(
                                f => f.Title!,
                                f => f.Subtitle!
                            )
                            .Query(query)
                            .Type(TextQueryType.Phrase)
                            .Slop(2) 
                          // phrase matching with slop
                         //  2 means up to two word positions can be swapped, inserted, or skipped between the query terms.
                        /*
                         * •	Query: "quick fox"
                            •	Document: "the quick brown fox"
                            •	With slop: 0, this does not match (because "quick" and "fox" are not adjacent).
                            •	With slop: 1, this still does not match (they are two positions apart).
                            •	With slop: 2, this does match (they are within two positions).
                         * 
                         */
                        ),
                        sh => sh
                        .Match(m => m
                            .Field(f => f.Category!)
                            .Query(query)
                            .Fuzziness("AUTO")
                        )
                    )
                    .MinimumShouldMatch(1)
                )
            )
        );

        return articles.Select(a => a.Id).ToList();
    }

    public async Task<bool> CreateArticleAsync(ElasticArticle article)
    {
        var response = await CreateAsync(_configuration.IndexName, article);

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

    public async Task<IReadOnlyCollection<ElasticArticle>> GetArticlesByDateRangeAsync(DateTime fromUtc, DateTime toUtc)
    {
        var articles = await SearchAsync<ElasticArticle>(_configuration.IndexName, s => s
            .Query(q => q
                .Range(r => r
                    .Date(dr => dr
                        .Field(f => f.CreatedOnUtc)
                        .Gte(fromUtc)
                        .Lte(toUtc)
                    )
                )
            )
        );

        return articles;
    }

    public async Task<IReadOnlyCollection<ElasticArticle>> GetArticlesByAuthorAsync(string authorQuery)
    {
        var articles = await SearchAsync<ElasticArticle>(_configuration.IndexName, s => s
            .Query(q => q
                .Bool(b => b
                    .Should(
                        sh => sh
                            .Match(m => m
                                .Field(f => f.Author!)
                                .Query(authorQuery)
                                .Fuzziness("AUTO")
                            ),
                        sh => sh
                            .MatchPhrase(m => m
                                .Field(f => f.Author!)
                                .Query(authorQuery)
                                .Slop(2)
                            )
                    )
                    .MinimumShouldMatch(1)
                )
            )
        );

        return articles;
    }
}
