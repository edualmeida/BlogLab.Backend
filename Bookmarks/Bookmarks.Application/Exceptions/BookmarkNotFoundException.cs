using Common.Application.Exceptions;

namespace Bookmarks.Application.Exceptions;
public class BookmarkNotFoundException(Guid bookmarkId) :
    NotFoundException("Bookmark", bookmarkId.ToString())
{

}
