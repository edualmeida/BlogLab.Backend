using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Delete;
public class DeleteArticleCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public class DeleteArticleCommandHandler(
        IArticlesDomainRepository articleRepository) 
        : IRequestHandler<DeleteArticleCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteArticleCommand request, 
            CancellationToken cancellationToken)
        {
            var article = await articleRepository.Find(request.Id, cancellationToken) ?? 
                throw new ArticleNotFoundException(request.Id);

            article.DisableArticle();

            await articleRepository.Save(article, cancellationToken);

            return Result.Success;
        }
    }
}