using Comments.Application.Comments.Queries.GetPaginated;
using MediatR;

namespace Comments.Application.Comments.Queries.Common;
internal sealed class GetRepositoryCommentsQuery : IRequest<GetCommentsResult>
{
    public Guid ArticleId { get; set; }


    public class GetRepositoryCommentsQueryHandler(
        ICommentsQueryRepository repository) : IRequestHandler<GetRepositoryCommentsQuery, GetCommentsResult>
    {
        public async Task<GetCommentsResult> Handle(
            GetRepositoryCommentsQuery request,
            CancellationToken cancellationToken)
        {
            var getResult = await repository.GetAll(request.ArticleId);

            return new GetCommentsResult
            {
                Comments = getResult,
            };
        }
    }

}
