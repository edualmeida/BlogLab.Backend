using Comments.Application.Contracts.Authors;
using Comments.Application.Services;
using MediatR;

namespace Comments.Application.Authors.Queries;
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
