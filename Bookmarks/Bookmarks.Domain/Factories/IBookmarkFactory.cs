public interface IBookmarkFactory : IBuilder<Bookmark>
{
    IBookmarkFactory WithCustomerId(Guid customerId);
    IBookmarkFactory WithArticleId(Guid articleId);
    Bookmark Build();
}
