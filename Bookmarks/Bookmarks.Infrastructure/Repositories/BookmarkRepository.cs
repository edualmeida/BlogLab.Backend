using AutoMapper;
using Bookmarks.Application.Bookmarks.Queries;
using Bookmarks.Application.Bookmarks.Queries.Common;
using Bookmarks.Domain.Models.Bookmarks;
using Bookmarks.Domain.Repositories;
using Bookmarks.Infrastructure.Persistence;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Infrastructure.Repositories;
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
        var article = await AllAsNoTracking().FirstOrDefaultAsync(x => x.ArticleId == articleId) 
            ?? throw new InvalidOperationException("Article not found for delete, id: " + articleId);

        Data.Remove(article);
        await Data.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BookmarkQueryResponse>> GetAll(CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<BookmarkQueryResponse>(AllAsNoTracking())
            .ToListAsync();
}
