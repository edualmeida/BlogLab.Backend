using Bookmarks.Application.Exceptions;
using Bookmarks.Domain.Repositories;
using Common.Application;
using Common.Application.Contracts;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Delete;
public class DeleteBookmarkCommand : IRequest<Result>
{
    public Guid ArticleId { get; set; }
    
    public class DeleteBookmarkCommandHandler(
        IBookmarkDomainRepository bookmarksRepository,
        ICurrentUserService currentUserService) 
        : IRequestHandler<DeleteBookmarkCommand, Result>
    {
        public async Task<Result> Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await bookmarksRepository
                .FindByArticleId(currentUserService.GetRequiredUserId(), request.ArticleId, cancellationToken) ??
                throw new BookmarkNotFoundException(request.ArticleId); // TODO: return result

            bookmark.DisableBookmark();

            await bookmarksRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}