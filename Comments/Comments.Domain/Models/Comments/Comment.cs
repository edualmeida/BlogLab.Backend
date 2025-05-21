using Common.Domain;
using Common.Domain.Models;

namespace Comments.Domain.Models.Comments;
public class Comment : Entity, IAggregateRoot
{
    internal Comment(
        string text,
        Guid articleId,
        Guid authorId)
    {
        Validate(text);
        Text = text;
        ArticleId = articleId;
        AuthorId = authorId;
    }

    public string Text { get; private set; }
    public Guid ArticleId { get; private set; }
    public Guid AuthorId { get; private set; }

    public Comment UpdateText(string text)
    {
        ValidateText(text);
        Text = text;
        return this;
    }

    public Comment UpdateArticle(Guid articleId)
    {
        ArticleId = articleId;
        return this;
    }

    public Comment DisableArticle()
    {
        Enabled = false;
        return this;
    }

    public Comment UpdateAuthorId(Guid authorId)
    {
        AuthorId = authorId;
        return this;
    }

    private static void Validate(string text)
    {
        ValidateText(text);
    }

    private static void ValidateText(string text)
        => Guard.ForStringLength(text, CommentModelConstants.MinTextLength, CommentModelConstants.MaxTextLength, nameof(Text));
}
