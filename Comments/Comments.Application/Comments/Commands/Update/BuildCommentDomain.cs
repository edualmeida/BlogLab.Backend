using Comments.Application.Comments.Commands.Common;
using Comments.Application.Comments.Exceptions;
using Comments.Domain.Models.Comments;
using Comments.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Update;
public class BuildCommentDomain(Guid id, CommentCommand command) 
    : IRequest<Result<Comment>>
{
    public Guid Id => id;
    public CommentCommand Comment => command;

    public class BuildArticleDomainHandler(ICommentDomainRepository repository) 
        : IRequestHandler<BuildCommentDomain, Result<Comment>>
    {
        public async Task<Result<Comment>> Handle(
            BuildCommentDomain request, 
            CancellationToken cancellationToken)
        {
            var domainComment = await repository.Find(request.Id);

            if(domainComment is null)
            {
                return Result<Comment>.Failure(new CommentNotFoundException(request.Id).Message);
            }

            domainComment.UpdateText(request.Comment.Text);

            return domainComment;
        }
    }
}