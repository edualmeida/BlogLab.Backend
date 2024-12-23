using MediatR;

public class CreateArticleCommand : ArticleCommand, IRequest<CreateArticleResponse>
{
    public class CreateArticleCommandHandler(
        IArticleDomainRepository articleRepository,
        IArticleBuilder articleFactory) : IRequestHandler<CreateArticleCommand, CreateArticleResponse>
    {
        public async Task<CreateArticleResponse> Handle(
            CreateArticleCommand request,
            CancellationToken cancellationToken)
        {
            var article = articleFactory
                .WithTitle(request.Title)
                .WithSubtitle(request.Subtitle)
                .WithText(request.Text)
                .WithThumbnailId(request.ThumbnailId)
                .WithColorId(request.ColorId)
                .WithCategoryId(request.CategoryId)
                .Build();

            await articleRepository.Save(article, cancellationToken);

            return new CreateArticleResponse(article.Id);
        }
    }
}