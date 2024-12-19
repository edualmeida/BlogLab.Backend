using AutoMapper;

public class ArticleQueryModel : ArticleModel, IMapFrom<Article>
{
    public override void Mapping(Profile mapper)
    {
        mapper.CreateMap<Article, ArticleQueryModel>()
            .ForMember(p => p.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.Name));
    }
}