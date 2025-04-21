using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Application.Articles.Commands.Create;
using ArticleCatalog.Application.Articles.Commands.Delete;
using ArticleCatalog.Application.Articles.Commands.Update;
using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Articles.Queries.GetPaginated;
using ArticleCatalog.Application.Articles.Queries.GetById;
using ArticleCatalog.Application.Articles.Queries.GetByIds;
using Common.Web;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleCatalog.Web.Controllers;
public class ArticlesController(IMediator mediator) : 
    ApiController(mediator)
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = $"{ApiKey.SchemeName}, {JwtBearerDefaults.AuthenticationScheme}")]
    public async Task<ActionResult<GetArticlesPaginatedResult>> GetAllPaginated(
        [FromQuery] GetArticlesPaginatedQuery paginatedQuery)
        => await Send(paginatedQuery);

    [HttpGet]
    [Route(Id)]
    [Authorize(AuthenticationSchemes = $"{ApiKey.SchemeName}, {JwtBearerDefaults.AuthenticationScheme}")]
    public async Task<ActionResult<ArticleQueryResponse>> GetById([FromRoute] GetArticleByIdQuery query)
        => await Send(query);
    
    [HttpPost]
    [Route("GetMany")]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<List<ArticleQueryResponse>>> GetByIds(ArticleGetByIdsQuery query)
        => await Send(query);

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateArticleResponse>> Create(CreateArticleCommand command)
        => await Send(command);

    [Authorize]
    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(Guid id, ArticleCommand command)
        => await Send(new UpdateArticleCommand(id, command));

    [Authorize]
    [HttpDelete]
    [Route(Id)]
    public async Task<ActionResult> Delete([FromRoute] DeleteArticleCommand command)
        => await Send(command);
}