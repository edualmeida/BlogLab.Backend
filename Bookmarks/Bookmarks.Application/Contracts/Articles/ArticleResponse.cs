public record PriceRequest(decimal Amount, string Currency);
public record WeightRequest(decimal Value, string Unit);

public class ArticleResponse
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string ManufacturerName { get; set; }
    public string CategoryName { get; set; }
    public string ColorName { get; set; }
    public PriceRequest Price { get; set; }
    public WeightRequest Weight { get; set; }
    public string Thumbnail { get; set; }
}