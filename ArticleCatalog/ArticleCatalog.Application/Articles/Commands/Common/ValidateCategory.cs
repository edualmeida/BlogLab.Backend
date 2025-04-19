using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Categories.Queries;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create.Validators;
internal sealed class ValidateCategory : IRequest<Result>
{
    public required Guid CategoryId{ get; set; }

    public class ValidateCategoryHandler(ICategoriesQueryRepository repository) : 
        IRequestHandler<ValidateCategory, Result>
    {
        public async Task<Result> Handle(ValidateCategory request, CancellationToken cancellationToken)
        {
            var response = await repository.GetById(request.CategoryId, cancellationToken);
            if (response is null)
            {
                return Result.Failure(new CategoryNotFoundException(request.CategoryId).Message);
            }

            return Result.Success;
        }
    }
}
