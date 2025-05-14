using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Domain.Repositories;
public interface IElasticArticleRepository
{
    Task<bool> CreateArticleAsync(Article article);
    Task<IReadOnlyCollection<Guid>> SearchArticlesByNameAsync(string name);
}
