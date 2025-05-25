using Comments.Domain.Models.Comments;
using Common.Domain;

namespace Comments.Domain.Repositories;
public interface ICommentDomainRepository : IDomainRepository<Comment>
{
    Task CreateAsync(Comment newItem);
    Task UpdateAsync(Guid id, Comment updateItem);
    Task DeleteAsync(Guid id);
    Task<Comment?> Find(Guid id);
}