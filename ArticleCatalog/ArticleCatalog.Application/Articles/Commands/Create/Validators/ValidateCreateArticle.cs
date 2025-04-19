using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create.Validators;
internal sealed class ValidateCreateArticle(CreateArticleCommand command) : IRequest<Result>
{
    public CreateArticleCommand Command => command;

    public class ValidateCreateArticleHandler(IMediator mediator) : 
        IRequestHandler<ValidateCreateArticle, Result>
    {
        public async Task<Result> Handle(ValidateCreateArticle request, CancellationToken cancellationToken)
        {
            return await VerifyCategoryExists(request.Command.CategoryId, cancellationToken);
        }

        private Task<Result> VerifyCategoryExists(Guid categoryId, CancellationToken cancellationToken)
        {
            return mediator.Send(new ValidateCategory { CategoryId = categoryId } , cancellationToken);
        }
    }
}
