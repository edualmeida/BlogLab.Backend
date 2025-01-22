using AutoMapper;
using Microsoft.EntityFrameworkCore;

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