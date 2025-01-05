using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class BookmarksController(ILogger<BookmarksController> logger, IMediator mediator) : ApiController(logger, mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<BookmarkArticleResponse>>> GetAll()
        => await Send(new BookmarkGetAllQuery());

    [HttpPost]
    public async Task<ActionResult<CreateBookmarkResponse>> Create(CreateBookmarkCommand command)
        => await Send(command);

    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(UpdateBookmarkCommand command)
        => await Send(command);

    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] DeleteBookmarkCommand command)
        => await Send(command);
}