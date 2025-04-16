using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Domain.Factories;
public interface IBookmarkFactory : IBuilder<Bookmark>
{
    IBookmarkFactory WithUserId(Guid customerId);
    IBookmarkFactory WithArticleId(Guid articleId);
}
