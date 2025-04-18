using Bookmarks.Application.Exceptions;
using Bookmarks.Application.Services;
using Bookmarks.Domain.Repositories;
using Common.Application.Contracts;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class CreateBookmarkValidator : IRequest
{
    public required CreateBookmarkCommand Command { get; set; }

    public class CreateBookmarkValidatorHandler(
        ICurrentUserService currentUserService,
        IBookmarkDomainRepository bookmarkRepository,
        IArticleCatalogHttpService articleCatalogHttpService)
        : IRequestHandler<CreateBookmarkValidator>
    {
        public async Task Handle(
            CreateBookmarkValidator request,
            CancellationToken cancellationToken)
        {
            var articles = await articleCatalogHttpService
                .GetArticlesByIds([request.Command.ArticleId]);

            if(articles.Count == 0)
            {
                throw new ArticleNotFoundException(request.Command.ArticleId);
            }

            var userId = currentUserService.GetRequiredUserId();
            var bookmark = await bookmarkRepository
                .FindByArticleId(userId, request.Command.ArticleId, cancellationToken);

            if(bookmark != null)
            {
                throw new BookmarkAlreadyExistException(userId, request.Command.ArticleId);
            }
        }
    }
}