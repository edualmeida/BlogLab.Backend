using AutoMapper;
using Microsoft.EntityFrameworkCore;

internal class ArticleRepository : DataRepository<BlogDbContext, Article>,
    IArticleDomainRepository,
    IArticleQueryRepository
{
    private readonly IMapper mapper;

    public ArticleRepository(BlogDbContext db, IMapper mapper)
        : base(db)
        => this.mapper = mapper;

    public async Task<Article?> Find(Guid id, CancellationToken cancellationToken = default)
        => await All()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<ArticleResponse> GetById(Guid id, CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<ArticleResponse>(AllAsNoTracking())
                .FirstAsync(b => b.Id == id, cancellationToken);

    public async Task<List<ArticleResponse>> GetAll(QueryOrderBy orderBy, CancellationToken cancellationToken = default)
        => (await mapper
            .ProjectTo<ArticleResponse>(AllAsNoTracking()
            .Include(b => b.Color)
            .Include(b => b.Category))
            //.ToOrdered(orderBy)) SQL Lite does not support decimal order by
            .ToListAsync(cancellationToken))
            .ToOrdered(orderBy);

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await Data.Articles.FindAsync(id);

        if (article == null)
            throw new ArgumentException("Article does not exist");

        Data.Articles.Remove(article);

        await Data.SaveChangesAsync(cancellationToken);
    }

    public async Task<Thumbnail> FindThumbnail(string name)
    {
        return await Data.Thumbnails.FirstAsync(x=>x.Name == name);
    }

    public async Task<Category> FindCategory(string name)
    {
        return await Data.Categories.FirstAsync(x => x.Name == name);
    }

    public async Task<Color> FindColor(string name)
    {
        return await Data.Colors.FirstAsync(x => x.Name == name);
    }
}

internal static class QueryableBikeExtesions
{
    public static List<ArticleResponse> ToOrdered(this List<ArticleResponse> query, QueryOrderBy direction) => direction switch
    {
        QueryOrderBy.Title => [.. query.OrderBy(x => x.Title)],
        _ => query,
    };
}