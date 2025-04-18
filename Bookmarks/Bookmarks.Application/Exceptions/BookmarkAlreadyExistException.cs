using Common.Application.Exceptions;

namespace Bookmarks.Application.Exceptions;
public class BookmarkAlreadyExistException(Guid userId, Guid articleId) :
    BadRequestException("Bookmark", $"UserId: {userId}, articleId: {articleId}")
{

}
