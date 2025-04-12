using Bookmarks.Application.Bookmarks.Commands.Common;
using Bookmarks.Domain.Factories;
using Bookmarks.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class CreateBookmarkCommand : BookmarkCommand, IRequest<Result>
{
    public class CreateBookmarkCommandHandler(
        IBookmarkDomainRepository bookmarkRepository,
        IBookmarkFactory bookmarkFactory)
        : IRequestHandler<CreateBookmarkCommand, Result>
    {
        public async Task<Result> Handle(
            CreateBookmarkCommand request,
            CancellationToken cancellationToken)
        {
            var bookmark = bookmarkFactory
                .WithArticleId(request.ArticleId)
                .WithCustomerId(request.UserId)
                .Build();
            
            await bookmarkRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}