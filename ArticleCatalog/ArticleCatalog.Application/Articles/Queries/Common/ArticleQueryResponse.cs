using ArticleCatalog.Application.Articles.Common;
using ArticleCatalog.Domain.Models.Articles;
using AutoMapper;

namespace ArticleCatalog.Application.Articles.Queries.Common;
public class ArticleQueryResponse : ArticleModel
{
    public required Guid Id { get; set; }
    public string Category { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    public string Author { get; set; } = "";
    public required Guid AuthorId { get; set; }
    public bool IsBookmarked { get; set; } = false;

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Article, ArticleQueryResponse>()
            .ForMember(p => p.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(p => p.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail!.Name))
            .ForMember(p => p.CreatedOn, opt => opt.MapFrom(src => src.CreatedOnUtc.ToLocalTime()))
            .ForMember(p => p.Author, opt => opt.MapFrom(src =>""))
            .ForMember(p => p.IsBookmarked, opt => opt.Ignore())
            .IncludeBase<Article, ArticleModel>();
}