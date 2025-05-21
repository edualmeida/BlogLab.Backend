using Comments.Application.Comments.Common;
using Comments.Domain.Models.Comments;
using AutoMapper;

namespace Comments.Application.Comments.Queries.Common;
public class CommentQueryResponse : CommentModel
{
    public required Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Author { get; set; } = "";

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Comment, CommentQueryResponse>()
            .ForMember(p => p.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(p => p.CreatedOn, opt => opt.MapFrom(src => src.CreatedOnUtc.ToLocalTime()))
            .ForMember(p => p.Author, opt => opt.MapFrom(src =>""))
            .IncludeBase<Comment, CommentModel>();
}