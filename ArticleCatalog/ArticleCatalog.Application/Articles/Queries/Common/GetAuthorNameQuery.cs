using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Services;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.Common;
public class GetAuthorNameQuery : IRequest<string>
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
