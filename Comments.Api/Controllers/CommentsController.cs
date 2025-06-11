using Comments.Application.Comments.Commands.Common;
using Comments.Application.Comments.Commands.Create;
using Comments.Application.Comments.Commands.Delete;
using Comments.Application.Comments.Commands.Update;
using Comments.Application.Comments.Queries.GetPaginated;
using Common.Web;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comments.Api.Controllers;
public class CommentsController(IMediator mediator) : 
    ApiController(mediator)
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = $"{ApiKeyConstants.SchemeName}, {JwtBearerDefaults.AuthenticationScheme}")]
    public async Task<ActionResult<GetCommentsResult>> GetComments(
        [FromQuery] GetCommentsQuery query)
        => await Send(query);

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateCommentResponse>> Create(CreateCommentCommand command)
        => await Send(command);

    [Authorize]
    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(Guid id, CommentCommand command)
        => await Send(new UpdateCommentCommand(id, command));

    [Authorize]
    [HttpDelete]
    [Route(Id)]
    public async Task<ActionResult> Delete([FromRoute] DeleteCommentCommand command)
        => await Send(command);
}