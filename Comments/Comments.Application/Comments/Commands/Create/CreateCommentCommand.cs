using Comments.Application.Comments.Commands.Common;
using Comments.Application.Comments.Commands.Create.Validators;
using Comments.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Create;
public class CreateCommentCommand : CommentCommand, IRequest<Result<CreateCommentResponse>>
{
    public class CreateCommentCommandHandler(
        IMediator mediator,
        ICommentDomainRepository repository) : 
        IRequestHandler<CreateCommentCommand, Result<CreateCommentResponse>>
    {
        public async Task<Result<CreateCommentResponse>> Handle(
            CreateCommentCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await mediator.Send(new ValidateCreateComment(request), cancellationToken);
            if (!validationResult.Succeeded)
            {
                return validationResult.Errors;
            }

            var comment = await mediator.Send(new BuildCommentDomain(request), cancellationToken);

            await repository.CreateAsync(comment);
            await mediator.Send(new CreateElasticComment(comment), cancellationToken);

            return new CreateCommentResponse(comment.Id);
        }
    }
}