using Bookmarks.Domain.Models.Bookmarks;
using Common.Domain;

namespace Bookmarks.Domain.Factories;
public interface IBookmarkFactory : IBuilder<Bookmark>
{
    IBookmarkFactory WithUserId(Guid customerId);
    IBookmarkFactory WithArticleId(Guid articleId);
}
