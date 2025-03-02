using AutoMapper;
using Bookmarks.Application.Bookmarks.Commands.Common;
using Bookmarks.Application.Bookmarks.Commands.Create;
using Common.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace Bookmarks.Web.Models;
public class CreateBookmarkRequest: BookmarkCommand, IMapTo<CreateBookmarkCommand>
{
    public CreateBookmarkCommand Map(IMapper mapper, HttpContext httpContext)
    {
        var result = mapper.Map<CreateBookmarkCommand>(this);
        
        result.UserId = httpContext.GetRequiredUserId();
        
        return result;
    }
    
    public static void CreateMapping(Profile mapper)
    {
        mapper.CreateMap<CreateBookmarkRequest, CreateBookmarkCommand>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}