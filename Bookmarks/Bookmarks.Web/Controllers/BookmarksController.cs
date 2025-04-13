using AutoMapper;
using Bookmarks.Application.Bookmarks.Commands.Delete;
using Bookmarks.Application.Bookmarks.Commands.Update;
using Bookmarks.Application.Bookmarks.Queries.Common;
using Bookmarks.Application.Bookmarks.Queries.GetByUserId;
using Bookmarks.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarks.Web.Controllers;
[Authorize]
public class BookmarksController(IMediator mediator, IMapper mapper)
    : ApiController(mediator)
{
    [HttpGet()]
    public async Task<ActionResult<List<BookmarkArticleQueryResponse>>> GetByUserId()
        =>  await Send(new BookmarkGetByUserIdQuery());

    [HttpPost]
    public async Task<ActionResult> Create(CreateBookmarkRequest request)
        => await Send(request.Map(mapper, HttpContext));

    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(UpdateBookmarkCommand command)
        => await Send(command);

    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] DeleteBookmarkCommand command)
        => await Send(command);
}