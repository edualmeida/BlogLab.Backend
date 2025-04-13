using ArticleCatalog.Application.Services;
using Common.Application.Contracts;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.Common;
public class IsBookmarkedQuery: IRequest<bool>
{
    public Guid ArticleId { get; set; }
    public class IsBookmarkedQueryHandler(
        ICurrentUserService currentUserService,
        IBookmarksHttpService bookmarksHttpService) : IRequestHandler<IsBookmarkedQuery, bool>
    {
        public async Task<bool> Handle(
            IsBookmarkedQuery request,
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
