using ArticleCatalog.Application.Contracts.Authors;
using ArticleCatalog.Application.Services;
using MediatR;

namespace ArticleCatalog.Application.Authors.Queries;
public class GetAuthorsQuery: IRequest<List<AuthorResponse>>
{

    public class GetAuthorsQueryHandler(
        IAuthorsHttpService authorsHttpService) : IRequestHandler<GetAuthorsQuery, List<AuthorResponse>>
    {
        public async Task<List<AuthorResponse>> Handle(
            GetAuthorsQuery request,
            CancellationToken cancellationToken)
        {
            return await authorsHttpService.GetAll(cancellationToken) ?? [];
        }
    }
}
