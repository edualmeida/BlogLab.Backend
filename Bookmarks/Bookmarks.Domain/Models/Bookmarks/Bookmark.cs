using Common.Domain;
using Common.Domain.Models;

namespace Bookmarks.Domain.Models.Bookmarks;

public class Bookmark(
    Guid userId, 
    Guid articleId) 
    : Entity, IAggregateRoot
{
    public Guid UserId => userId;
    public Guid ArticleId => articleId;

    public Bookmark DisableBookmark()
    {
        Enabled = false;
        return this;
    }
}
