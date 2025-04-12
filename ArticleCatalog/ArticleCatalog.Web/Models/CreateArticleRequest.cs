using AutoMapper;
using Common.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace ArticleCatalog.Web.Models;
public class CreateArticleRequest : ArticleCommand, IMapTo<CreateArticleCommand>
{
    public CreateArticleCommand Map(IMapper mapper, HttpContext httpContext)
    {
        var result = mapper.Map<CreateArticleCommand>(this);
        
        result.SetAuthorId(httpContext.GetRequiredUserId());
        
        return result;
    }
    public static void CreateMapping(Profile mapper)
    {
        mapper.CreateMap<CreateArticleRequest, CreateArticleCommand>()
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    }
}