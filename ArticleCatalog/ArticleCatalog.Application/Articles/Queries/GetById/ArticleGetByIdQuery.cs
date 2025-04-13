using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Contracts.Authors;
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
            var article = await GetArticle(request.Id, cancellationToken);

            article.Author = await GetAuthorName(article.AuthorId, cancellationToken);
            article.IsBookmarked = await GetIsBookmarked(article.Id, cancellationToken);

            return article;
        }

        private async Task<bool> GetIsBookmarked(Guid articleId, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            if (userId.HasValue)
            {
                var userBookmarks = await bookmarksHttpService.GetUserBookmarks(cancellationToken);
                return userBookmarks.Any(x => x.Bookmark.ArticleId == articleId);
            }

            return false;
        }

        private async Task<string> GetAuthorName(Guid authorId, CancellationToken cancellationToken)
        {
            return (await authorsHttpService.GetById(authorId, cancellationToken))?.FirstName ??
                throw new AuthorNotFoundException(authorId);
        }

        private async Task<ArticleQueryResponse> GetArticle(Guid articleId, CancellationToken cancellationToken)
        {
            return await articleRepository.GetById(articleId, cancellationToken) ?? 
                throw new ArticleNotFoundException(articleId);
        }
    }
}