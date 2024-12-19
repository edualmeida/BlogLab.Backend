using AutoMapper;

public class BookmarkResponse : BookmarkModel
{
    public Guid Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Bookmark, BookmarkResponse>()
            .IncludeBase<Bookmark, BookmarkModel>();
}