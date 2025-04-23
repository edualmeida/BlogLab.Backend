using Bookmarks.Application.Bookmarks.Commands.Create;
using Bookmarks.Application.Bookmarks.Commands.Delete;
using Bookmarks.Application.Bookmarks.Queries.Common;
using Bookmarks.Application.Bookmarks.Queries.GetByUserId;
using Common.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarks.Web.Controllers;
[Authorize]
public class BookmarksController(IMediator mediator)
    : ApiController(mediator)
{
    [HttpGet()]
    public async Task<ActionResult<List<BookmarkArticleQueryResponse>>> GetByUserId()
        =>  await Send(new BookmarkGetByUserIdQuery());

    [HttpPost]
    public async Task<ActionResult> Create(CreateBookmarkCommand command)
        => await Send(command);

    [HttpDelete]
    [Route("{ArticleId}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteBookmarkCommand command)
        => await Send(command);
}