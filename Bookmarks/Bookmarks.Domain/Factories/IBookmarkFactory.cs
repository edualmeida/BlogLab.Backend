public interface IBookmarkFactory : IFactory<Bookmark>
{
    IBookmarkFactory WithCustomerId(Guid customerId);
    IBookmarkFactory WithBikeId(Guid articleId);
    Bookmark Build();
}
