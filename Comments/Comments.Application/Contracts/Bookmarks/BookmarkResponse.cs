namespace Comments.Application.Contracts.Bookmarks;
public sealed class BookmarkResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ArticleId { get; set; }
}