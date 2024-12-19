using AutoMapper;
using Microsoft.EntityFrameworkCore;

internal class BookmarkRepository : DataRepository<BookmarksDbContext, Bookmark>,
    IBookmarkDomainRepository,
    IBookmarkQueryRepository
{
    private readonly IMapper mapper;

    public BookmarkRepository(BookmarksDbContext db, IMapper mapper)
        : base(db)
    {
        this.mapper = mapper;
    }

    public Task<Bookmark> Find(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid articleId, CancellationToken cancellationToken = default)
    {
        var article = AllAsNoTracking().FirstOrDefault(x => x.ArticleId == articleId) 
            ?? throw new InvalidOperationException("Article not found for delete, id: " + articleId);

        Data.Remove(article);
        await Data.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BookmarkResponse>> GetAll(CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<BookmarkResponse>(AllAsNoTracking())
            .ToListAsync();
}
