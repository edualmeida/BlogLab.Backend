using ArticleCatalog.Domain.Models.Categories;
using ArticleCatalog.Infrastructure.Persistence;
using AutoMapper;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArticleCatalog.Infrastructure.Repositories;
internal class CategoriesRepository : DataRepository<ArticleCatalogDbContext, Category>,
    ICategoriesQueryRepository
{
    private readonly IMapper mapper;

    public CategoriesRepository(ArticleCatalogDbContext db, IMapper mapper)
        : base(db)
        => this.mapper = mapper;

    public async Task<CategoryResponse> GetById(Guid id, CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<CategoryResponse>(AllAsNoTracking())
                .FirstAsync(b => b.Id == id, cancellationToken);

    public async Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken = default)
        => (await mapper
            .ProjectTo<CategoryResponse>(AllAsNoTracking())
                .ToListAsync(cancellationToken));
}