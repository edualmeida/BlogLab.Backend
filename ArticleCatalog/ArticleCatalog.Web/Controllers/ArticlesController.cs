using ArticleCatalog.Application.Articles.Commands.Update;
using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
using ArticleCatalog.Application.Articles.Queries.GetById;
using ArticleCatalog.Application.Articles.Queries.GetByIds;
using ArticleCatalog.Web.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleCatalog.Web.Controllers;
public class ArticlesController(IMediator mediator, IMapper mapper) : 
    ApiController(mediator)
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<ArticleGetAllPaginatedResult>> GetAllPaginated(
        [FromQuery] ArticleGetAllPaginatedQuery paginatedQuery)
        => await Send(paginatedQuery);

    [HttpGet]
    [Route(Id)]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<ArticleQueryResponse>> GetById([FromRoute] ArticleGetByIdQuery query)
        => await Send(query);
    
    [HttpPost]
    [Route("get/multiple")]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<List<ArticleQueryResponse>>> GetByIds(ArticleGetByIdsQuery query)
        => await Send(query);

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateArticleResponse>> Create(CreateArticleRequest request)
        => await Send(request.Map(mapper, HttpContext));

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