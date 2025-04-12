using Bookmarks.Application.Bookmarks.Commands.Common;
using Bookmarks.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Update;
public class UpdateBookmarkCommand : BookmarkCommand, IRequest<Result>
{
    public Guid Id { get; set; }
    
    public class UpdateBookmarkCommandHandler : IRequestHandler<UpdateBookmarkCommand, Result>
    {
        private readonly IBookmarkDomainRepository bookmarksRepository;

        public UpdateBookmarkCommandHandler(IBookmarkDomainRepository bookmarkRepository)
        {
            this.bookmarksRepository = bookmarkRepository;
        }

        public async Task<Result> Handle(UpdateBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await bookmarksRepository.Find(request.Id, cancellationToken);

            await bookmarksRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}