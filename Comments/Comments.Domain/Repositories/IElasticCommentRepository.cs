using Comments.Domain.Models.Comments;

namespace Comments.Domain.Repositories;
public interface IElasticCommentRepository
{
    Task<bool> CreateCommentAsync(ElasticComment comment);
}