using Bookmarks.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class CreateBookmarkCommand : IRequest<Result>
{
    public Guid ArticleId { get; set; }

    public class CreateBookmarkCommandHandler(
        IMediator mediator,
        IBookmarkDomainRepository bookmarkRepository)
        : IRequestHandler<CreateBookmarkCommand, Result>
    {
        public async Task<Result> Handle(
            CreateBookmarkCommand request,
            CancellationToken cancellationToken)
        {
            await mediator.Send(new CreateBookmarkValidator { Command = request }, cancellationToken);

            var bookmark = await mediator.Send(new BuildBookmarkDomain { ArticleId = request.ArticleId }, cancellationToken);

            await bookmarkRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}