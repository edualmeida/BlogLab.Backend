using MediatR;

public class DeleteArticleCommand : ArticleCommand, IRequest<Result>
{
    public Guid Id { get; set; }

    public class DeleteArticleCommandHandler(IArticleDomainRepository articleRepository) 
        : IRequestHandler<DeleteArticleCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteArticleCommand request, 
            CancellationToken cancellationToken)
        {
            var article = await articleRepository.Find(request.Id, cancellationToken);

            article.DisableArticle();

            await articleRepository.Save(article, cancellationToken);

            return Result.Success;
        }
    }
}