using AutoMapper;

public record PriceResponse(decimal Amount, string Currency);
public record WeightResponse(decimal Value, string Unit);

public class BookmarkArticleResponse: IMapFrom<ArticleResponse>
{
    public Guid ArticleId { get; set; }
    public string Model { get; set; }
    public string ManufacturerName { get; set; }
    public string CategoryName { get; set; }
    public string ColorName { get; set; }
    public PriceResponse Price { get; set; }
    public WeightResponse Weight { get; set; }
    public string Thumbnail { get; set; }

    public void Mapping(Profile mapper)
    => mapper
        .CreateMap<ArticleResponse, BookmarkArticleResponse>()
        .ForMember(p => p.ArticleId, opt => opt.MapFrom(src => src.Id))
        .ForMember(p => p.Price, opt => opt.MapFrom(src => new PriceResponse(src.Price.Amount, src.Price.Currency)))
        .ForMember(p => p.Weight, opt => opt.MapFrom(src => new WeightResponse(src.Weight.Value, src.Weight.Unit)));
}