using MediatR;

public class UpdateArticleCommand(Guid id, ArticleCommand command) : IRequest<Result>
{
    public Guid Id => id;
    public ArticleCommand Article => command;

    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Result>
    {
        private readonly IArticleDomainRepository articleRepository;

        public UpdateArticleCommandHandler(IArticleDomainRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<Result> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.Find(request.Id, cancellationToken);

            article.UpdateTitle(request.Article.Title);
            article.UpdateSubtitle(request.Article.Subtitle);
            article.UpdateText(request.Article.Text);
            article.UpdateCategory(request.Article.CategoryId);
            article.UpdateThumbnail(request.Article.ThumbnailId);

            await articleRepository.Save(article, cancellationToken);

            return Result.Success;
        }
    }
}