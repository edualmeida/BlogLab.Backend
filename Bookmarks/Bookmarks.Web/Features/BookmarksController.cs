using Microsoft.AspNetCore.Mvc;

public class BookmarksController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<List<BookmarkBikeResponse>>> GetAll()
        => await Send(new BookmarkGetAllQuery());

    [HttpPost]
    public async Task<ActionResult<CreateBookmarkResponse>> Create(CreateBookmarkCommand command)
        => await Send(command);

    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(UpdateBookmarkCommand command)
        => await Send(command);

    [HttpDelete]
    [Route("{ArticleId}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteBookmarkCommand command)
        => await Send(command);
}