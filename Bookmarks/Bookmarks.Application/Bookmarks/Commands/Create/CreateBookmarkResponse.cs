namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class CreateBookmarkResponse(Guid bookmarkId)
{
    public Guid BookmarkId => bookmarkId;
}