using Comments.Application.Comments.Commands.Common;
using Comments.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Update;
public class UpdateCommentCommand(Guid id, CommentCommand command) 
    : IRequest<Result>
{
    public Guid Id => id;
    public CommentCommand Comment => command;

    public class UpdateCommentCommandHandler(IMediator mediator, ICommentDomainRepository repository) 
        : IRequestHandler<UpdateCommentCommand, Result>
    {
        public async Task<Result> Handle(
            UpdateCommentCommand updateCommand, 
            CancellationToken cancellationToken)
        {
            var validationResult = await mediator.Send(new ValidateUpdateComment(updateCommand.Comment), cancellationToken);
            if(!validationResult.Succeeded)
            {
                return validationResult;
            }

            var domainArticle = await mediator.Send(new BuildCommentDomain(updateCommand.Id, updateCommand.Comment), cancellationToken);
            if (!domainArticle.Succeeded)
            {
                return domainArticle;
            }

            await repository.CreateAsync(domainArticle.Data);

            return Result.Success;
        }
    }
}