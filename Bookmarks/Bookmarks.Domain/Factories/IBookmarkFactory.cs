public interface IBookmarkFactory : IFactory<Bookmark>
{
    IBookmarkFactory WithCustomerId(Guid customerId);
    IBookmarkFactory WithArticleId(Guid articleId);
    Bookmark Build();
}
