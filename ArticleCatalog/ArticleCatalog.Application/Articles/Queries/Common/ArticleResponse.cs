using AutoMapper;

public class ArticleResponse : ArticleModel
{
    public Guid Id { get; set; }
    public string Category { get; set; } = "";
    public string Color { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    public string Author { get; set; } = "";
    public Guid AuthorId { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Article, ArticleResponse>()
            .ForMember(p => p.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(p => p.Color, opt => opt.MapFrom(src => src.Color!.Name))
            .ForMember(p => p.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail!.Name))
            .ForMember(p => p.CreatedOn, opt => opt.MapFrom(src => src.CreatedOnUTC.ToLocalTime()))
            .ForMember(p => p.Author, opt => opt.MapFrom(src =>""))
            .IncludeBase<Article, ArticleModel>();
}