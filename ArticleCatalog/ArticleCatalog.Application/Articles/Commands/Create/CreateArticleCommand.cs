using MediatR;

public class CreateArticleCommand : ArticleCommand, IRequest<CreateArticleResponse>
{
    public class CreateArticleCommandHandler(
        IArticleDomainRepository articleRepository,
        IArticleFactory articleFactory) : IRequestHandler<CreateArticleCommand, CreateArticleResponse>
    {
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