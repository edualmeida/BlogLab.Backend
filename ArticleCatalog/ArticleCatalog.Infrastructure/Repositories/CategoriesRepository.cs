using ArticleCatalog.Application.Categories.Queries;
using ArticleCatalog.Application.Categories.Queries.Common;
using ArticleCatalog.Domain.Models.Categories;
using ArticleCatalog.Infrastructure.Persistence;
using AutoMapper;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArticleCatalog.Infrastructure.Repositories;
internal class CategoriesRepository(ArticleCatalogDbContext db, IMapper mapper)
    : DataRepository<ArticleCatalogDbContext, Category>(db),
    ICategoriesQueryRepository
{
    public async Task<CategoryResponse?> GetById(Guid id, CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<CategoryResponse?>(AllAsNoTracking()
                .Where(x => x.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

    public async Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken = default)
        => (await mapper
            .ProjectTo<CategoryResponse>(AllAsNoTracking())
                .ToListAsync(cancellationToken));
}