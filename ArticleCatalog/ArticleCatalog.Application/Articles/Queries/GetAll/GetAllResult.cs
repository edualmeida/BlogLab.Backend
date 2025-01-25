public class GetAllResult
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<ArticleResponse> Articles { get; set; } = [];
}