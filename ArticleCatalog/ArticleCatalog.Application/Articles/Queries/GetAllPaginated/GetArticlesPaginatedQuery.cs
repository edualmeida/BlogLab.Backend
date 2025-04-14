using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Authors.Queries;
using ArticleCatalog.Application.Bookmarks.Queries;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
public class GetArticlesPaginatedQuery : IRequest<GetArticlesPaginatedResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public class ArticleAllQueryHandler(IMediator mediator) : 
        IRequestHandler<GetArticlesPaginatedQuery, GetArticlesPaginatedResult>
    {
        public async Task<GetArticlesPaginatedResult> Handle(
            GetArticlesPaginatedQuery request, 
            CancellationToken cancellationToken)
        {
            var userBookmarks = mediator.Send(new GetUserBookmarksQuery(), cancellationToken);
            var authors = mediator.Send(new GetAuthorsQuery(), cancellationToken);
            var getResult = mediator.Send(new GetRepositoryArticlesQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, cancellationToken);

            await Task.WhenAll(getResult, userBookmarks, authors);

            getResult.Result.Articles.ForEach(article =>
            {
                article.IsBookmarked = userBookmarks.Result.Any(x => x.Bookmark.ArticleId == article.Id);
                article.Author = authors.Result
                    .FirstOrDefault(a => a.Id == article.AuthorId)?
                    .FirstName ?? "ND";
            });

            return getResult.Result;
        }
    }
}