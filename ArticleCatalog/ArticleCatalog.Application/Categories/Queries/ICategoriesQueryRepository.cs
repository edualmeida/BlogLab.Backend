using ArticleCatalog.Domain.Models.Categories;

public interface ICategoriesQueryRepository : IQueryRepository<Category>
{
    Task<CategoryResponse> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken = default);
}