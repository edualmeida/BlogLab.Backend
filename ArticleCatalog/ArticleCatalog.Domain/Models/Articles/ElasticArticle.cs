namespace ArticleCatalog.Domain.Models.Articles;
public sealed class ElasticArticle
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Text { get; set; }
    public string? Category { get; set; }
    public string? Author { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
