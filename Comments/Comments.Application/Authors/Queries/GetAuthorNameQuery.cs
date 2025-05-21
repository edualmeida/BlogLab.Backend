using Comments.Application.Comments.Exceptions;
using Comments.Application.Services;
using MediatR;

namespace Comments.Application.Authors.Queries;
internal sealed class GetAuthorNameQuery : IRequest<string>
{
    public Guid AuthorId { get; set; }

    public class GetAuthorNameQueryHandler(
        IAuthorsHttpService authorsHttpService) : IRequestHandler<GetAuthorNameQuery, string>
    {
        public async Task<string> Handle(
            GetAuthorNameQuery request,
            CancellationToken cancellationToken)
        {
            return (await authorsHttpService.GetById(request.AuthorId, cancellationToken))?.FirstName ??
                throw new AuthorNotFoundException(request.AuthorId);
        }
    }
}
