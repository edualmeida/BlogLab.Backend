using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController(
    IMediator mediator) : ControllerBase
{
    protected const string Id = "{id}";
    protected const string PathSeparator = "/";

    protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
        => mediator.Send(request).ToActionResult();

    protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
        => mediator.Send(request).ToActionResult();

    protected Task<ActionResult> Send(IRequest<Result> request)
        => mediator.Send(request).ToActionResult();
}