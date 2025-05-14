using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Domain.Repositories;
public interface IElasticArticleRepository
{
    Task<bool> CreateArticleAsync(ElasticArticle article);
    Task<IReadOnlyCollection<Guid>> SearchArticlesAsync(string query);
}
