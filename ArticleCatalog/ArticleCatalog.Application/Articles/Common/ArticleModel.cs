using ArticleCatalog.Domain.Models.Articles;
using AutoMapper;

public class ArticleModel : IMapFrom<Article>
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Text { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ThumbnailId { get; set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<Article, ArticleModel>();
    }
}