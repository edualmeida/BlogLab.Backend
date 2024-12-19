using AutoMapper;

public class ArticleResponse : ArticleQueryModel
{
    public Guid Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Article, ArticleResponse>()
            .IncludeBase<Article, ArticleQueryModel>();
}