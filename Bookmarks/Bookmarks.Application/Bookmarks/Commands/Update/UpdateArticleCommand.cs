using MediatR;

public class UpdateBookmarkCommand : BookmarkCommand, IRequest<Result>
{
    public Guid Id { get; set; }
    
    public class UpdateBookmarkCommandHandler : IRequestHandler<UpdateBookmarkCommand, Result>
    {
        private readonly IBookmarkDomainRepository bookmarksRepository;

        public UpdateBookmarkCommandHandler(IBookmarkDomainRepository BookmarkRepository)
        {
            this.bookmarksRepository = BookmarkRepository;
        }

        public async Task<Result> Handle(UpdateBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await bookmarksRepository.Find(request.Id, cancellationToken);

            await bookmarksRepository.Save(bookmark, cancellationToken);

            return Result.Success;
        }
    }
}