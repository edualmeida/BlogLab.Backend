using Comments.Domain.Builders;
using Common.Application.Contracts;
using MediatR;
using Comments.Domain.Models.Comments;

namespace Comments.Application.Comments.Commands.Create;
public sealed class BuildCommentDomain(CreateCommentCommand command) : IRequest<Comment>
{
    public CreateCommentCommand Command => command;

    public sealed class BuildCommentDomainHandler(
        ICurrentUserService currentUserService,
        ICommentBuilder builder) : IRequestHandler<BuildCommentDomain, Comment>
    {
        public Task<Comment> Handle(
            BuildCommentDomain request,
            CancellationToken cancellationToken)
        {
            var comment = builder
                .WithText(request.Command.Text)
                .WithArticleId(request.Command.ArticleId)
                .WithAuthorId(currentUserService.GetRequiredUserId())
                .Build();

            return Task.FromResult(comment);
        }
    }
}
