using MediatR;

public class CreateArticleCommand : ArticleCommand, IRequest<CreateArticleResponse>
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleResponse>
    {
        private readonly IArticleDomainRepository articleRepository;
        private readonly IArticleFactory articleFactory;

        public CreateArticleCommandHandler(
            IArticleDomainRepository articleRepository,
            IArticleFactory articleFactory)
        {
            this.articleRepository = articleRepository;
            this.articleFactory = articleFactory;
        }

        public async Task<CreateArticleResponse> Handle(
            CreateArticleCommand request,
            CancellationToken cancellationToken)
        {
            var article = articleFactory
                .WithTitle(request.Title)
                .WithThumbnail(request.Thumbnail)
                .WithColor(request.Color)
                .WithCategory(request.Category)
                .Build();

            await articleRepository.Save(article, cancellationToken);

            return new CreateArticleResponse(article.Id);
        }
    }
}