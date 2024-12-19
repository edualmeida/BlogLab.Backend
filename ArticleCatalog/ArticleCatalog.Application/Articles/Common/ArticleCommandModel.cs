using AutoMapper;

public class ArticleCommandModel
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Text { get; set; }
    public Guid Category { get; set; }
    public Guid Color { get; set; }
    public Guid Thumbnail { get; set; }
}