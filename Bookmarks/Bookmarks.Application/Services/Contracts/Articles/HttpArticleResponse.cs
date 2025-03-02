namespace Bookmarks.Application.Services.Contracts.Articles;
public class HttpArticleResponse
{
    public Guid Id { get; set; }
    public string Category { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    public string Author { get; set; } = "";
    public Guid AuthorId { get; set; }
}