using System.Text.Json.Serialization;

namespace Comments.Domain.Models.Comments;
public sealed class ElasticComment
{
    public Guid Id { get; set; }
    public string? Text { get; set; }

    [JsonPropertyName("articleId")]
    public Guid ArticleId { get; set; }
    public string? Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
