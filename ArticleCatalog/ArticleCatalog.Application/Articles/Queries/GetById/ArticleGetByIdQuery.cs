using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Services;
using Common.Application.Contracts;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class ArticleGetByIdQuery : EntityCommand, IRequest<ArticleQueryResponse>
{
    public class ArticleDetailsQueryHandler(
        ICurrentUserService currentUserService,
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService,
        IBookmarksHttpService bookmarksHttpService) : IRequestHandler<ArticleGetByIdQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            ArticleGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetById(request.Id, cancellationToken) ??
                throw new ArticleNotFoundException(request.Id);
            var author = await authorsHttpService.GetById(article.AuthorId, cancellationToken) ??
                throw new AuthorNotFoundException(article.AuthorId);

            article.Author = author.FirstName;

            List<UserBookmarkResponse> userBookmarks = [];
            var userId = currentUserService.GetUserId();
            if (userId.HasValue)
            {
                userBookmarks = await bookmarksHttpService.GetUserBookmarks(userId.Value, cancellationToken);
            }

            article.IsBookmarked = userBookmarks?.Any(x => x.ArticleId == article.Id);
            
            return article;
        }
    }
}