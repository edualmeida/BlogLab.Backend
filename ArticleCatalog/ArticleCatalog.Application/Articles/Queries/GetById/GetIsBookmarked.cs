using ArticleCatalog.Application.Services;
using Common.Application.Contracts;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;
internal sealed class GetIsBookmarked: IRequest<bool>
{
    public Guid ArticleId { get; set; }
    public class IsBookmarkedQueryHandler(
        ICurrentUserService currentUserService,
        IBookmarksHttpService bookmarksHttpService) : IRequestHandler<GetIsBookmarked, bool>
    {
        public async Task<bool> Handle(
            GetIsBookmarked request,
            CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            if (userId.HasValue)
            {
                var userBookmarks = await bookmarksHttpService.GetUserBookmarks(cancellationToken);
                return userBookmarks.Any(x => x.Bookmark.ArticleId == request.ArticleId);
            }

            return false;
        }
    }
}
