using Comments.Application.Comments.Exceptions;
using Comments.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Delete;
public class DeleteCommentCommand : IRequest<Result>
{
    public Guid Id { get; set; }

    public class DeleteCommentCommandHandler(
        ICommentDomainRepository repository) 
        : IRequestHandler<DeleteCommentCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteCommentCommand request, 
            CancellationToken cancellationToken)
        {
            var comment = await repository.Find(request.Id) ?? 
                throw new CommentNotFoundException(request.Id);

            comment.DisableArticle();

            await repository.CreateAsync(comment);

            return Result.Success;
        }
    }
}