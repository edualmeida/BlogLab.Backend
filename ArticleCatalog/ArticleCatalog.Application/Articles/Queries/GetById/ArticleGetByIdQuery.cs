using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Services;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class ArticleGetByIdQuery : EntityCommand, IRequest<ArticleQueryResponse>
{
    public Guid? UserId { get; set; }
    public class ArticleDetailsQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService,
        IBookmarksHttpService bookmarksHttpService) : IRequestHandler<ArticleGetByIdQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            ArticleGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetById(request.Id, cancellationToken);

            if (article == null)
            {
                throw new ArticleNotFoundException(request.Id);
            }

            var author = await authorsHttpService.GetById(article.AuthorId, cancellationToken);
            if (author == null)
            {
                throw new AuthorNotFoundException(article.AuthorId);
            }

            article.Author = author.FirstName;

            List<UserBookmarkResponse> userBookmarks = new();
            if (request.UserId.HasValue)
            {
                userBookmarks = await bookmarksHttpService.GetUserBookmarks(request.UserId.Value);
            }

            article.IsBookmarked = userBookmarks?.Any(x => x.ArticleId == article.Id);
            
            return article;
        }
    }
}