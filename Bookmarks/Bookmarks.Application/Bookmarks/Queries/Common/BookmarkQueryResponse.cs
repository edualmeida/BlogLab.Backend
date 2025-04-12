using AutoMapper;
using Bookmarks.Application.Bookmarks.Common;
using Bookmarks.Domain.Models.Bookmarks;

namespace Bookmarks.Application.Bookmarks.Queries.Common;
public class BookmarkQueryResponse : BookmarkModel
{
    public Guid Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Bookmark, BookmarkQueryResponse>()
            .IncludeBase<Bookmark, BookmarkModel>();
}