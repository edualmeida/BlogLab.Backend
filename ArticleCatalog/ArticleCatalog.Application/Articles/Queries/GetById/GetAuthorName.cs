using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Services;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;
internal sealed class GetAuthorName : IRequest<string>
{
    public Guid AuthorId { get; set; }
    public class GetAuthorNameQueryHandler(
        IAuthorsHttpService authorsHttpService) : IRequestHandler<GetAuthorName, string>
    {
        public async Task<string> Handle(
            GetAuthorName request,
            CancellationToken cancellationToken)
        {
            return (await authorsHttpService.GetById(request.AuthorId, cancellationToken))?.FirstName ??
                throw new AuthorNotFoundException(request.AuthorId);
        }
    }

}
