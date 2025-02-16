using AutoMapper;
using Microsoft.EntityFrameworkCore;

internal class ArticleRepository : DataRepository<ArticleCatalogDbContext, Article>,
    IArticleDomainRepository,
    IArticleQueryRepository
{
    private readonly IMapper mapper;

    public ArticleRepository(ArticleCatalogDbContext db, IMapper mapper)
        : base(db)
        => this.mapper = mapper;

    public async Task<Article?> Find(Guid id, CancellationToken cancellationToken = default)
        => await All()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<ArticleResponse> GetById(Guid id, CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<ArticleResponse>(AllAsNoTracking())
                .FirstAsync(b => b.Id == id, cancellationToken);

    public async Task<GetAllResult> GetAll(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var articles = await mapper
            .ProjectTo<ArticleResponse>(AllAsNoTracking()
                .Where(x => x.Enabled)
                .OrderBy(x => x.CreatedOnUTC)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize))
            .ToListAsync(cancellationToken);

        var totalCount = await AllAsNoTracking()
            .Where(x => x.Enabled)
            .CountAsync(cancellationToken);

        return new GetAllResult
        {
            Articles = articles,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await Data.Articles.FindAsync(id);

        if (article == null)
            throw new ArgumentException("Article does not exist");

        Data.Articles.Remove(article);

        await Data.SaveChangesAsync(cancellationToken);
    }
}