using Bookmarks.Application.Exceptions;
using Bookmarks.Application.Services;
using Bookmarks.Domain.Factories;
using Bookmarks.Domain.Repositories;
using Common.Application;
using Common.Application.Contracts;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class CreateBookmarkCommand : IRequest<Result>
{
    public Guid ArticleId { get; set; }

    public class CreateBookmarkCommandHandler(
        ICurrentUserService currentUserService,
        IBookmarkDomainRepository bookmarkRepository,
        IBookmarkFactory bookmarkFactory,
        IArticleCatalogHttpService articleCatalogHttpService)
        : IRequestHandler<CreateBookmarkCommand, Result>
    {
        public async Task<Result> Handle(
            CreateBookmarkCommand request,
            CancellationToken cancellationToken)
        {
            var articles = await articleCatalogHttpService
                .GetArticlesByIds([request.ArticleId]);

            if(articles.Count == 0)
            {
                throw new ArticleNotFoundException(request.ArticleId);
            }

            var bookmark = bookmarkFactory
                .WithArticleId(request.ArticleId)
                .WithUserId(currentUserService.GetRequiredUserId())
                .Build();
            
            await bookmarkRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}