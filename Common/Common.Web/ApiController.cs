using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController(ILogger logger, IMediator mediator) : ControllerBase
{
    protected const string Id = "{id}";
    protected const string PathSeparator = "/";

    protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
    {
        logger.LogInformation("Send request: " + request.GetType());
        return mediator.Send(request).ToActionResult();
    }

    protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
        => mediator.Send(request).ToActionResult();

    protected Task<ActionResult> Send(IRequest<Result> request)
        => mediator.Send(request).ToActionResult();
}