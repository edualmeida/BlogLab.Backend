using Comments.Application.Comments.Exceptions;
using MediatR;

namespace Comments.Application.Comments.Queries.Common;
internal sealed class GetRepositoryCommentQuery : IRequest<CommentQueryResponse>
{
    public Guid CommentId { get; set; }

    public class GetRepositoryCommentQueryHandler(
        ICommentsQueryRepository commentRepository) : 
        IRequestHandler<GetRepositoryCommentQuery, CommentQueryResponse>
    {
        public async Task<CommentQueryResponse> Handle(
            GetRepositoryCommentQuery request,
            CancellationToken cancellationToken)
        {
            return await commentRepository.GetById(request.CommentId) ??
                throw new CommentNotFoundException(request.CommentId);
        }
    }

}
