using AutoMapper;

public class ArticleResponse : ArticleModel
{
    public Guid Id { get; set; }
    public string Category { get; set; } = "";
    public string Color { get; set; } = "";
    public string Thumbnail { get; set; } = "";

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Article, ArticleResponse>()
            .ForMember(p => p.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(p => p.Color, opt => opt.MapFrom(src => src.Color!.Name))
            .ForMember(p => p.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail!.Name))
            .IncludeBase<Article, ArticleModel>();
}