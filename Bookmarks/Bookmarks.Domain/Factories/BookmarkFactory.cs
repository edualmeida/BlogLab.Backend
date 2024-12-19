internal class BookmarkFactory : IBookmarkFactory
{
    private Guid customerId = default!;
    private Guid articleId = default!;

    private bool isCustomerIdSet = false;
    private bool isBikeIdSet = false;

    public IBookmarkFactory WithCustomerId(Guid customerId)
    {
        this.customerId = customerId;
        isCustomerIdSet = true;

        return this;
    }

    public IBookmarkFactory WithBikeId(Guid articleId)
    {
        this.articleId = articleId;
        isBikeIdSet = true;

        return this;
    }

    public Bookmark Build()
    {
        if (!isCustomerIdSet || !isBikeIdSet)
            throw new InvalidOperationException("Customer ID, article Id must have a value.");

        return new Bookmark(customerId, articleId);
    }
}