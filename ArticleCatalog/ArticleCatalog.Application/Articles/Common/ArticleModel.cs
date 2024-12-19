using AutoMapper;

public record PriceRequest(decimal Amount, string Currency);

public record WeightRequest(decimal Value, string Unit);

public class ArticleModel : IMapFrom<Article>
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Text { get; set; }
    public string Thumbnail { get; set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<Article, ArticleModel>();
    }
}