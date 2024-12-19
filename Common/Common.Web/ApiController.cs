using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected const string Id = "{id}";
    protected const string PathSeparator = "/";

    private IMediator? mediator;

    protected IMediator Mediator
        => mediator ??= HttpContext
            .RequestServices
            .GetService<IMediator>()!;

    protected Task<ActionResult<TResult>> Send<TResult>(
        IRequest<TResult> request)
        => Mediator.Send(request).ToActionResult();

    protected Task<ActionResult<TResult>> Send<TResult>(
        IRequest<Result<TResult>> request)
        => Mediator.Send(request).ToActionResult();

    protected Task<ActionResult> Send(
        IRequest<Result> request)
        => Mediator.Send(request).ToActionResult();
}