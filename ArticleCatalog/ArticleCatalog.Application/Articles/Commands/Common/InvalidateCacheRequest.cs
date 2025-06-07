using Common.Application;
using Common.Domain;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Common;
internal class InvalidateCacheRequest : IRequest<Result>
{
    public required string CacheKey { get; set; }

    public class InvalidateCacheHandler(ICacheRepository cacheRepository) : IRequestHandler<InvalidateCacheRequest, Result>
    {
        public async Task<Result> Handle(InvalidateCacheRequest request, CancellationToken cancellationToken)
        {
            var isSuccess = await cacheRepository.DeleteAsync(request.CacheKey);

            if (!isSuccess)
            {
                return Result.Failure($"Failed to invalidate cache for key: {request.CacheKey}");
            }

            return Result.Success;
        }
    }
}