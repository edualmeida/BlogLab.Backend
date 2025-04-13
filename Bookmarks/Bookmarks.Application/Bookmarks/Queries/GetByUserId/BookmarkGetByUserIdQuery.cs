using AutoMapper;
using Bookmarks.Application.Bookmarks.Queries.Common;
using Bookmarks.Application.Services;
using Common.Application.Contracts;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Queries.GetByUserId;
public class BookmarkGetByUserIdQuery() : IRequest<List<BookmarkArticleQueryResponse>>
{
    public class BookmarkGetAllQueryHandler(
        ICurrentUserService currentUserService,
        IBookmarkQueryRepository bookmarkRepository,
        IArticleCatalogHttpService articleCatalogHttpService,
        IMapper mapper): IRequestHandler<BookmarkGetByUserIdQuery, List<BookmarkArticleQueryResponse>>
    {
        public async Task<List<BookmarkArticleQueryResponse>> Handle(
            BookmarkGetByUserIdQuery request, 
            CancellationToken cancellationToken)
        {
            var bookmarks = await bookmarkRepository.GetByUserId(currentUserService.GetRequiredUserId(), cancellationToken);
            if(bookmarks.Count == 0)
            {
                return [];
            }

            var articles = await articleCatalogHttpService
                .GetArticlesByIds(bookmarks
                    .Select(x => x.ArticleId));
            
            var response = new List<BookmarkArticleQueryResponse>();

            foreach (var bookmark in bookmarks)
            {
                response.Add(new BookmarkArticleQueryResponse()
                    {
                        Bookmark    = bookmark,
                        Article = mapper.Map<ArticleResponse>(articles.Single(x => x.Id == bookmark.ArticleId))
                    });
            }

            return response;
        }
    }
}