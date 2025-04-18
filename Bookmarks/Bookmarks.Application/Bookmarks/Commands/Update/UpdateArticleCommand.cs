using Bookmarks.Application.Bookmarks.Commands.Common;
using Bookmarks.Application.Exceptions;
using Bookmarks.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Update;
public class UpdateBookmarkCommand : BookmarkCommand, IRequest<Result>
{
    public Guid Id { get; set; }
    
    public class UpdateBookmarkCommandHandler(IBookmarkDomainRepository bookmarkRepository) : 
        IRequestHandler<UpdateBookmarkCommand, Result>
    {
        public async Task<Result> Handle(UpdateBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await bookmarkRepository.Find(request.Id, cancellationToken);

            if(bookmark is null)
            {
                throw new BookmarkNotFoundException(request.Id);
            }

            await bookmarkRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}