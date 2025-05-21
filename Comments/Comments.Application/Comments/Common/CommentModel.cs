using Comments.Domain.Models.Comments;
using AutoMapper;
using Common.Application.Mapping;

namespace Comments.Application.Comments.Common;
public class CommentModel : IMapFrom<Comment>
{
    public string Text { get; set; } = "";
    public Guid ArticleId { get; set; }
    public Guid AuthorId { get; set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<Comment, CommentModel>();
    }
}