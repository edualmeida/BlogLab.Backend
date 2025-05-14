using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Application.Articles.Commands.Create.Validators;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create;
public class CreateArticleCommand : ArticleCommand, IRequest<Result<CreateArticleResponse>>
{
    public class CreateArticleCommandHandler(
        IMediator mediator,
        IArticleDomainRepository articleRepository) : 
        IRequestHandler<CreateArticleCommand, Result<CreateArticleResponse>>
    {
        public async Task<Result<CreateArticleResponse>> Handle(
            CreateArticleCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await mediator.Send(new ValidateCreateArticle(request), cancellationToken);
            if (!validationResult.Succeeded)
            {
                return validationResult.Errors;
            }

            var article = await mediator.Send(new BuildArticleDomain(request), cancellationToken);
            var articleId = await articleRepository.Save(article);

            await mediator.Send(new PutArticleIndex(article), cancellationToken);

            return new CreateArticleResponse(articleId);
        }
    }
}