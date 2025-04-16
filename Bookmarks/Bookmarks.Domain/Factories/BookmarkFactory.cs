using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Domain.Factories;
internal class BookmarkFactory : IBookmarkFactory
{
    private Guid userId = Guid.Empty;
    private Guid articleId = Guid.Empty;

    private bool isUserIdSet = false;
    private bool isArticleIdSet = false;

    public IBookmarkFactory WithUserId(Guid customerId)
    {
        this.userId = customerId;
        isUserIdSet = true;

        return this;
    }

    public IBookmarkFactory WithArticleId(Guid articleId)
    {
        this.articleId = articleId;
        isArticleIdSet = true;

        return this;
    }

    public Bookmark Build()
    {
        if (!isUserIdSet || !isArticleIdSet)
            throw new InvalidOperationException("Customer ID, article Id must have a value.");

        return new Bookmark(userId, articleId);
    }
}