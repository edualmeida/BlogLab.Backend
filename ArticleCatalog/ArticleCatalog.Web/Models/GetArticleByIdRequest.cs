using ArticleCatalog.Application.Articles.Queries.GetById;
using AutoMapper;
using Common.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace ArticleCatalog.Web.Models;

public class GetArticleByIdRequest : EntityCommand, IMapTo<ArticleGetByIdQuery>
{
    public ArticleGetByIdQuery Map(IMapper mapper, HttpContext httpContext)
    {
        var result = mapper.Map<ArticleGetByIdQuery>(this);
        
        result.UserId = httpContext.GetUserId();
        
        return result;
    }
    public static void CreateMapping(Profile mapper)
    {
        mapper.CreateMap<GetArticleByIdRequest, ArticleGetByIdQuery>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}