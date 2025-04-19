using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Categories.Queries.Common;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Categories.Queries.GetById;
public class CategoryGetByIdQuery : IRequest<Result<CategoryResponse>>
{
    public Guid Id { get; set; }
    public class CategoryGetByIdQueryHandler(
        ICategoriesQueryRepository repository
        ) : IRequestHandler<CategoryGetByIdQuery, Result<CategoryResponse>>
    {
        public async Task<Result<CategoryResponse>> Handle(
            CategoryGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var response = await repository.GetById(request.Id, cancellationToken);
            if (response is null)
            {
                return Result<CategoryResponse>.Failure(new CategoryNotFoundException(request.Id).Message);
            }

            return response;
        }
    }
}