using MediatR;

public class UpdateArticleCommand : ArticleCommand, IRequest<Result>
{
    public Guid Id { get; set; }
    
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

            article.UpdateTitle(request.Text);

            await articleRepository.Save(article, cancellationToken);

            return Result.Success;
        }
    }
}