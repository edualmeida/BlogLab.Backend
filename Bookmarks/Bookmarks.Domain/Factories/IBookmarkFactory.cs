using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Domain.Factories;
public interface IBookmarkFactory : IBuilder<Bookmark>
{
    IBookmarkFactory WithCustomerId(Guid customerId);
    IBookmarkFactory WithArticleId(Guid articleId);
}
