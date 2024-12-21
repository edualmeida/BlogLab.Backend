using MediatR;
using Microsoft.AspNetCore.Mvc;

public class ArticlesController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<ArticleResponse>>> GetAll([FromQuery] ArticleGetAllQuery query)
        => await Send(query);

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<ArticleResponse>> GetById([FromRoute] ArticleGetByIdQuery query)
        => await Send(query);
    
    [HttpPost]
    public async Task<ActionResult<CreateArticleResponse>> Create(CreateArticleCommand command)
        => await Send(command);
    
    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(UpdateArticleCommand command)
        => await Send(command);
}