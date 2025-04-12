using ArticleCatalog.Application.Categories.Queries.Common;
using ArticleCatalog.Domain.Models.Categories;

namespace ArticleCatalog.Application.Categories.Queries;
public interface ICategoriesQueryRepository : IQueryRepository<Category>
{
    Task<CategoryResponse> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken = default);
}