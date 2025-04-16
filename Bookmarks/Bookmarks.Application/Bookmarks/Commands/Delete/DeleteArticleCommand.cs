using Bookmarks.Application.Exceptions;
using Bookmarks.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Delete;
public class DeleteBookmarkCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    
    public class DeleteBookmarkCommandHandler(IBookmarkDomainRepository bookmarksRepository) 
        : IRequestHandler<DeleteBookmarkCommand, Result>
    {
        public async Task<Result> Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await bookmarksRepository.Find(request.Id, cancellationToken) ??
                throw new BookmarkNotFoundException(request.Id);

            bookmark.DisableArticle();

            await bookmarksRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}