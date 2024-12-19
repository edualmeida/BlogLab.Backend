using AutoMapper;

public class BookmarkModel : IMapFrom<Bookmark>
{
    public Guid CustomerId { get; set; }
    public Guid ArticleId { get; set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<Bookmark, BookmarkModel>();
    }
}
