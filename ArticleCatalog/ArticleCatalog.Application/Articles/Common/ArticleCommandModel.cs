using AutoMapper;

public class ArticleCommandModel
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Text { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ColorId { get; set; }
    public Guid ThumbnailId { get; set; }
}