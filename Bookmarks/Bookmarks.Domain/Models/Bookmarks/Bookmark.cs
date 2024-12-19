public class Bookmark : Entity, IAggregateRoot
{
    public Bookmark(Guid createdBy, Guid articleId)
    {
        CreatedBy = createdBy;
        ArticleId = articleId;

        //RaiseEvent(new BookmarkAddedEvent());
    }

    public Guid CreatedBy { get; private set; }
    public Guid ArticleId { get; private set; }
}
