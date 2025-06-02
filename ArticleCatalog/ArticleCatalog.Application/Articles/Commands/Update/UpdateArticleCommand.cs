using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Update;
public class UpdateArticleCommand(Guid id, ArticleCommand articleCommand) 
    : IRequest<Result>
{
    public Guid Id => id;
    public ArticleCommand Article => articleCommand;

    public class UpdateArticleCommandHandler(IMediator mediator, IArticlesDomainRepository articleRepository) 
        : IRequestHandler<UpdateArticleCommand, Result>
    {
        public async Task<Result> Handle(
            UpdateArticleCommand updateArticleCommand, 
            CancellationToken cancellationToken)
        {
            var validationResult = await mediator.Send(new ValidateUpdateArticle(updateArticleCommand.Article), cancellationToken);
            if(!validationResult.Succeeded)
            {
                return validationResult;
            }

            var domainArticle = await mediator.Send(new BuildArticleDomain(updateArticleCommand.Id, updateArticleCommand.Article), cancellationToken);
            if (!domainArticle.Succeeded)
            {
                return domainArticle;
            }

            await articleRepository.Save(domainArticle.Data, cancellationToken);

            return Result.Success;
        }
    }
}