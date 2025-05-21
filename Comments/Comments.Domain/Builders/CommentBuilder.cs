using Comments.Domain.Models.Comments;

namespace Comments.Domain.Builders;
internal class CommentBuilder : ICommentBuilder
{
    private string commentText = default!;
    private Guid commentArticleId;
    private Guid commentAuthorId;

    private bool isTextSet = false;
    private bool isArticleSet = false;
    private bool isAuthorSet = false;


    public ICommentBuilder WithText(string text)
    {
        commentText = text;
        isTextSet = true;

        return this;
    }

    public ICommentBuilder WithArticleId(Guid articleId)
    {
        commentArticleId = articleId;
        isArticleSet = true;

        return this;
    }

    public ICommentBuilder WithAuthorId(Guid authorId)
    {
        this.commentAuthorId = authorId;
        isAuthorSet = true;

        return this;
    }

    public Comment Build()
    {
        if (!isArticleSet || !isAuthorSet || !isTextSet)
            throw new InvalidOperationException("text, authorId, articleId must have a value.");

        return new Comment(
            commentText,
            commentArticleId,
            commentAuthorId);
    }
}