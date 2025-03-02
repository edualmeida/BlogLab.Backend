using ArticleCatalog.Application.Articles.Queries;
using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using ArticleCatalog.Infrastructure.Persistence;
using AutoMapper;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArticleCatalog.Infrastructure.Repositories;
internal class ArticleRepository(
    ArticleCatalogDbContext db, 
    IMapper mapper) 
    : DataRepository<ArticleCatalogDbContext, Article>(db),
    IArticleDomainRepository, IArticleQueryRepository
{
    public async Task<Article?> Find(Guid id, CancellationToken cancellationToken = default)
        => await All()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<ArticleQueryResponse?> GetById(Guid id, CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<ArticleQueryResponse?>(AllAsNoTracking()
                .Where(x => x.Id == id))
            .FirstOrDefaultAsync(cancellationToken);
    
    public async Task<List<ArticleQueryResponse>> GetByIds(
        List<Guid> ids, CancellationToken cancellationToken = default)
        => await mapper
            .ProjectTo<ArticleQueryResponse>(AllAsNoTracking()
                .Where(b => ids.Contains(b.Id)))
            .ToListAsync(cancellationToken);

    public async Task<ArticleGetAllPaginatedResult> GetAll(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var articles = await mapper
            .ProjectTo<ArticleQueryResponse>(AllAsNoTracking()
                .Where(x => x.Enabled)
                .OrderBy(x => x.CreatedOnUtc)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize))
            .ToListAsync(cancellationToken);

        var totalCount = await AllAsNoTracking()
            .Where(x => x.Enabled)
            .CountAsync(cancellationToken);

        return new ArticleGetAllPaginatedResult
        {
            Articles = articles,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await Data.Articles.FindAsync(id, cancellationToken);

        if (article == null)
            throw new ArgumentException("Article does not exist");

        Data.Articles.Remove(article);

        await Data.SaveChangesAsync(cancellationToken);
    }
}