using ArticleCatalog.Application.Articles.Queries.GetPaginated;
using Common.Domain;
using MediatR;

namespace ArticleCatalog.Application.Behaviors;
public class GetArticlesCacheBehavior(
    ICacheRepository<GetArticlesPaginatedResult> cacheRepository) : 
    IPipelineBehavior<GetArticlesPaginatedQuery, GetArticlesPaginatedResult>
{
    private const string CacheKey = "GetArticlesPaginatedQuery";

    public async Task<GetArticlesPaginatedResult> Handle(
        GetArticlesPaginatedQuery request, 
        RequestHandlerDelegate<GetArticlesPaginatedResult> next, 
        CancellationToken cancellationToken)
    {
        if(request.PageNumber != 1)
        {
            return await next(); // Cache only the first page
        }

        var cachedResult = await cacheRepository.GetAsync(CacheKey);
        if (cachedResult != null)
        {
            return cachedResult;
        }
        var result = await next();
        await cacheRepository.SetAsync(CacheKey, result, TimeSpan.FromMinutes(5));
        
        return result;
    }
}
