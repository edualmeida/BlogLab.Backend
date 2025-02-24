using AutoMapper;
using MediatR;

public class BookmarkGetAllQuery : EntityCommand, IRequest<List<BookmarkArticleResponse>>
{
    public class BookmarkGetAllQueryHandler(
        IBookmarkQueryRepository bookmarkRepository,
        IArticleCatalogHttpService articleCatalogHttpService,
        IMapper mapper): IRequestHandler<BookmarkGetAllQuery, List<BookmarkArticleResponse>>
    {
        public async Task<List<BookmarkArticleResponse>> Handle(
            BookmarkGetAllQuery request, 
            CancellationToken cancellationToken)
        {
            var bookmarks = await bookmarkRepository.GetAll(cancellationToken);
            if(bookmarks.Count == 0)
            {
                return [];
            }

            var articles = await articleCatalogHttpService.GetArticlesByIds(bookmarks.Select(x => x.ArticleId.ToString()));

            return mapper.Map<List<BookmarkArticleResponse>>(articles.Where(x=> bookmarks.Exists(f=>f.ArticleId == x.Id)));
        }
    }
}