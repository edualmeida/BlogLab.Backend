using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Domain.Factories;
internal class BookmarkFactory : IBookmarkFactory
{
    private Guid customerId = default!;
    private Guid articleId = default!;

    private bool isCustomerIdSet = false;
    private bool isArticleIdSet = false;

    public IBookmarkFactory WithCustomerId(Guid customerId)
    {
        this.customerId = customerId;
        isCustomerIdSet = true;

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
        if (!isCustomerIdSet || !isArticleIdSet)
            throw new InvalidOperationException("Customer ID, article Id must have a value.");

        return new Bookmark(customerId, articleId);
    }
}