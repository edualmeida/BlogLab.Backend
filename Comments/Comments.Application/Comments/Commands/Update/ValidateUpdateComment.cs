using Comments.Application.Comments.Commands.Common;
using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Update;
public class ValidateUpdateComment(CommentCommand command) 
    : IRequest<Result>
{
    public CommentCommand Comment => command;

    public class ValidateUpdateCommentHandler(IMediator mediator)
        : IRequestHandler<ValidateUpdateComment, Result>
    {
        public async Task<Result> Handle(
            ValidateUpdateComment request, 
            CancellationToken cancellationToken)
        {

            return Result.Success;
        }
    }
}