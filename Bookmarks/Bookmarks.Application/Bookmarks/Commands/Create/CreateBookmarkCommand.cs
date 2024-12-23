using MediatR;

public class CreateBookmarkCommand : BookmarkCommand, IRequest<CreateBookmarkResponse>
{
    public class CreateBookmarkCommandHandler : IRequestHandler<CreateBookmarkCommand, CreateBookmarkResponse>
    {
        private readonly IBookmarkDomainRepository bookmarksRepository;
        private readonly IBookmarkFactory bookmarkFactory;

        public CreateBookmarkCommandHandler(
            IBookmarkDomainRepository bookmarkRepository,
            IBookmarkFactory bookmarkFactory)
        {
            this.bookmarksRepository = bookmarkRepository;
            this.bookmarkFactory = bookmarkFactory;
        }

        public async Task<CreateBookmarkResponse> Handle(
            CreateBookmarkCommand request,
            CancellationToken cancellationToken)
        {
            var bookmark = bookmarkFactory
                .WithArticleId(request.ArticleId)
                .WithCustomerId(request.CustomerId)
                .Build();
            
            await bookmarksRepository.Save(bookmark, cancellationToken);

            return new CreateBookmarkResponse(bookmark.Id);
        }
    }
}