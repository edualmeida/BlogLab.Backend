using AutoMapper;
using Bookmarks.Domain.Models.Bookmarks;
using Common.Application.Mapping;

namespace Bookmarks.Application.Bookmarks.Common;
public class BookmarkModel : IMapFrom<Bookmark>
{
    public Guid UserId { get; set; }
    public Guid ArticleId { get; set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<Bookmark, BookmarkModel>();
    }
}
