using ArticleCatalog.Application.Services;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
public class GetAllPaginatedQuery : IRequest<GetAllPaginatedResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public Guid? UserId { get; set; }

    public class ArticleAllQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService,
        IBookmarksHttpService bookmarksHttpService
        ) : IRequestHandler<GetAllPaginatedQuery, GetAllPaginatedResult>
    {
        public async Task<GetAllPaginatedResult> Handle(
            GetAllPaginatedQuery request, 
            CancellationToken cancellationToken)
        {
            var getResult = await articleRepository.GetAll(
                request.PageNumber, 
                request.PageSize, 
                cancellationToken);

            var userBookmarks = request.UserId.HasValue ? 
                await bookmarksHttpService.GetUserBookmarks(cancellationToken) : [];

            var authors = await authorsHttpService.GetAll(cancellationToken);
            getResult.Articles.ForEach(article =>
            {
                article.Author = authors
                    .FirstOrDefault(a => a.Id == article.AuthorId)?.FirstName ?? "ND";

                article.IsBookmarked = userBookmarks?.Any(x => x.Bookmark.ArticleId == article.Id) ?? false;
            });

            return getResult;
        }
    }
}