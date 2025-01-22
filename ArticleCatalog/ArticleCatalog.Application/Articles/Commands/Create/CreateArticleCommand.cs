using MediatR;

public class CreateArticleCommand : ArticleCommand, IRequest<CreateArticleResponse>
{
    public class CreateArticleCommandHandler(
        IArticleDomainRepository articleRepository,
        IArticleBuilder articleBuilder) : IRequestHandler<CreateArticleCommand, CreateArticleResponse>
    {
        public async Task<CreateArticleResponse> Handle(
            CreateArticleCommand request,
            CancellationToken cancellationToken)
        {
            var article = articleBuilder
                .WithTitle(request.Title)
                .WithSubtitle(request.Subtitle)
                .WithText(request.Text)
                .WithCategoryId(request.CategoryId)
                .WithThumbnailId(new Guid("48AA27CA-2EAF-4CBD-B744-B84F045E066D"))
                .Build();

            await articleRepository.Save(article, cancellationToken);

            return new CreateArticleResponse(article.Id);
        }
    }
}