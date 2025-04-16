using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Categories.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Categories.Queries.GetAll;
public class CategoryGetByIdQuery : IRequest<CategoryResponse>
{
    public Guid Id { get; set; }
    public class CategoryGetByIdQueryHandler(
        ICategoriesQueryRepository repository
        ) : IRequestHandler<CategoryGetByIdQuery, CategoryResponse>
    {
        public async Task<CategoryResponse> Handle(
            CategoryGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            return (await repository.GetById(request.Id, cancellationToken)) ??
                throw new CategoryNotFoundException(request.Id);
        }
    }
}