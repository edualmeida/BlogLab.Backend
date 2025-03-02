using AutoMapper;
using Bookmarks.Application.Bookmarks.Queries.Common;
using Bookmarks.Application.Services;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Queries.GetAll;
public class BookmarkGetAllQuery : EntityCommand, IRequest<List<BookmarkArticleQueryResponse>>
{
    public class BookmarkGetAllQueryHandler(
        IBookmarkQueryRepository bookmarkRepository,
        IArticleCatalogHttpService articleCatalogHttpService,
        IMapper mapper): IRequestHandler<BookmarkGetAllQuery, List<BookmarkArticleQueryResponse>>
    {
        public async Task<List<BookmarkArticleQueryResponse>> Handle(
            BookmarkGetAllQuery request, 
            CancellationToken cancellationToken)
        {
            var bookmarks = await bookmarkRepository.GetAll(cancellationToken);
            if(bookmarks.Count == 0)
            {
                return [];
            }

            var articles = await articleCatalogHttpService
                .GetArticlesByIds(bookmarks
                    .Select(x => x.ArticleId.ToString()));
            
            var response = new List<BookmarkArticleQueryResponse>();

            foreach (var bookmark in bookmarks)
            {
                response.Add(new BookmarkArticleQueryResponse(bookmark,
                    mapper.Map<ArticleQueryResponse>(
                        articles.Single(x => x.Id == bookmark.ArticleId))));
            }

            return mapper.Map<List<BookmarkArticleQueryResponse>>(response);
        }
    }
}