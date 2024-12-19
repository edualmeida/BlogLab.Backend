using MediatR;

public class DeleteBookmarkCommand : IRequest<Result>
{
    public Guid ArticleId { get; set; }
    
    public class DeleteBookmarkCommandHandler : IRequestHandler<DeleteBookmarkCommand, Result>
    {
        private readonly IBookmarkDomainRepository bookmarksRepository;

        public DeleteBookmarkCommandHandler(IBookmarkDomainRepository BookmarkRepository)
        {
            this.bookmarksRepository = BookmarkRepository;
        }

        public async Task<Result> Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            await bookmarksRepository.Delete(request.ArticleId, cancellationToken);

            return Result.Success;
        }
    }
}