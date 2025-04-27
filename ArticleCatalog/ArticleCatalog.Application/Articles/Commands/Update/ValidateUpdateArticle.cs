using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Application.Articles.Commands.Create.Validators;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Update;
public class ValidateUpdateArticle(ArticleCommand articleCommand) 
    : IRequest<Result>
{
    public ArticleCommand Article => articleCommand;

    public class ValidateUpdateArticleHandler(IMediator mediator)
        : IRequestHandler<ValidateUpdateArticle, Result>
    {
        public async Task<Result> Handle(
            ValidateUpdateArticle request, 
            CancellationToken cancellationToken)
        {
            var categoryResult = await mediator.Send(new ValidateCategory 
            { 
                CategoryId = request.Article.CategoryId 
            }, cancellationToken);

            if(!categoryResult.Succeeded)
            {
                return categoryResult;
            }

            return Result.Success;
        }
    }
}