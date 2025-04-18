using ArticleCatalog.Application.Categories.Queries.GetAll;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create.Validators;
internal sealed class CreateArticleValidator : IRequest
{
    public required CreateArticleCommand Command { get; set; }

    public class CreateArticleValidatorHandler(IMediator mediator) : IRequestHandler<CreateArticleValidator>
    {
        public async Task Handle(CreateArticleValidator request, CancellationToken cancellationToken)
        {
            await VerifyCategoryExists(request.Command.CategoryId, cancellationToken);
        }

        private async Task VerifyCategoryExists(Guid categoryId, CancellationToken cancellationToken)
        {
            await mediator.Send(new CategoryGetByIdQuery { Id = categoryId }, cancellationToken);
        }
    }
}
