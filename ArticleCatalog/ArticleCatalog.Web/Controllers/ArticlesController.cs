using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ArticlesController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<GetAllResult>> GetAll([FromQuery] ArticleGetAllQuery query)
        => await Send(query);

    [HttpGet]
    [Route(Id)]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<ArticleResponse>> GetById([FromRoute] ArticleGetByIdQuery query)
        => await Send(query);

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateArticleResponse>> Create(CreateArticleRequest request)
        => await Send(request.ToCommand(GetUserId()));

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