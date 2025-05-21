using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Create.Validators;
internal sealed class ValidateCreateComment(CreateCommentCommand command) : IRequest<Result>
{
    public CreateCommentCommand Command => command;

    public class ValidateCreateCommentHandler(IMediator mediator) : 
        IRequestHandler<ValidateCreateComment, Result>
    {
        public Task<Result> Handle(ValidateCreateComment request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success);
        }
    }
}
