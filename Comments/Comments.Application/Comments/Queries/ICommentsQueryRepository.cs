using Comments.Application.Comments.Queries.Common;
using Comments.Domain.Models.Comments;
using Common.Application.Contracts;

namespace Comments.Application.Comments.Queries;
public interface ICommentsQueryRepository : IQueryRepository<Comment>
{
    Task<CommentQueryResponse?> GetById(
        Guid id);

    Task<List<CommentQueryResponse>> GetAll(Guid articleId);
}