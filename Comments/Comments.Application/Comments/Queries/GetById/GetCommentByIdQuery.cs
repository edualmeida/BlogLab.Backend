using Comments.Application.Authors.Queries;
using Comments.Application.Comments.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Comments.Queries.GetById;

public class GetCommentByIdQuery : EntityCommand, IRequest<CommentQueryResponse>
{
    public class GetCommentByIdQueryHandler(IMediator mediator) : 
        IRequestHandler<GetCommentByIdQuery, CommentQueryResponse>
    {
        public async Task<CommentQueryResponse> Handle(
            GetCommentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await mediator.Send(new GetRepositoryCommentQuery { CommentId = request.Id }, cancellationToken);

            article.Author = await mediator.Send(new GetAuthorNameQuery { AuthorId = article.AuthorId }, cancellationToken);

            return article;
        }
    }
}