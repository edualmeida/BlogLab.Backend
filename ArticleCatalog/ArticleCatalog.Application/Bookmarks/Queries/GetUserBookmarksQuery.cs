using ArticleCatalog.Application.Contracts.Bookmarks;
using ArticleCatalog.Application.Services;
using Common.Application.Contracts;
using MediatR;

namespace ArticleCatalog.Application.Bookmarks.Queries;
public class GetUserBookmarksQuery : IRequest<List<UserBookmarkResponse>>
{

    public class GetUserBookmarksQueryHandler(
        ICurrentUserService currentUserService,
        IBookmarksHttpService bookmarksHttpService) : IRequestHandler<GetUserBookmarksQuery, List<UserBookmarkResponse>>
    {
        public async Task<List<UserBookmarkResponse>> Handle(
            GetUserBookmarksQuery request,
            CancellationToken cancellationToken)
        {
            var userBookmarks = currentUserService.GetUserId().HasValue ?
                await bookmarksHttpService.GetUserBookmarks(cancellationToken) : [];

            return userBookmarks;
        }
    }
}
