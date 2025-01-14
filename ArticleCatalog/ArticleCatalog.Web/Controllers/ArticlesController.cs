using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
public class ArticlesController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<ArticleResponse>>> GetAll()
        => await Send(new ArticleGetAllQuery());

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<ArticleResponse>> GetById([FromRoute] ArticleGetByIdQuery query)
        => await Send(query);

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateArticleResponse>> Create(CreateArticleCommand command)
        => await Send(command);
    
    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(Guid id, ArticleCommand command)
        => await Send(new UpdateArticleCommand(id, command));

    [HttpDelete]
    [Route(Id)]
    public async Task<ActionResult> Delete([FromRoute] DeleteArticleCommand command)
        => await Send(command);
}