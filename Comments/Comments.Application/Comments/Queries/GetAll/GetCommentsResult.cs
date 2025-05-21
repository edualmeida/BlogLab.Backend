using Comments.Application.Comments.Queries.Common;

namespace Comments.Application.Comments.Queries.GetPaginated;
public class GetCommentsResult
{
    public List<CommentQueryResponse> Comments { get; set; } = [];
}