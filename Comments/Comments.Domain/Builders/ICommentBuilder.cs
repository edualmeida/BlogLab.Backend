using Comments.Domain.Models.Comments;
using Common.Domain;

namespace Comments.Domain.Builders;
public interface ICommentBuilder : IBuilder<Comment>
{
    ICommentBuilder WithText(string text);
    ICommentBuilder WithArticleId(Guid articleId);
    ICommentBuilder WithAuthorId(Guid authorId);
}