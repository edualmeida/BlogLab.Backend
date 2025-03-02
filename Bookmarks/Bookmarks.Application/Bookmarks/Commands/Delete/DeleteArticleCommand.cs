using Bookmarks.Domain.Repositories;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Delete;
public class DeleteBookmarkCommand : IRequest<Result>
{
    public Guid BookmarkId { get; set; }
    
    public class DeleteBookmarkCommandHandler : IRequestHandler<DeleteBookmarkCommand, Result>
    {
        private readonly IBookmarkDomainRepository bookmarksRepository;

        public DeleteBookmarkCommandHandler(IBookmarkDomainRepository BookmarkRepository)
        {
            this.bookmarksRepository = BookmarkRepository;
        }

        public async Task<Result> Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            await bookmarksRepository.Delete(request.BookmarkId, cancellationToken);

            return Result.Success;
        }
    }
}