using Comments.Application.Comments.Exceptions;
using Comments.Application.Contracts.Authors;
using Comments.Application.Services;
using MediatR;

namespace Comments.Application.Authors.Queries;
internal sealed class GetAuthorQuery : IRequest<AuthorResponse>
{
    public Guid AuthorId { get; set; }

    public class GetAuthorQueryHandler(
        IAuthorsHttpService authorsHttpService) : IRequestHandler<GetAuthorQuery, AuthorResponse>
    {
        public async Task<AuthorResponse> Handle(
            GetAuthorQuery request,
            CancellationToken cancellationToken)
        {
            return (await authorsHttpService.GetById(request.AuthorId, cancellationToken)) ??
                throw new AuthorNotFoundException(request.AuthorId);
        }
    }
}
