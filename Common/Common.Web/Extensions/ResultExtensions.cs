using System.Net.Mime;
using Common.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Common.Web.Extensions;
public static class ResultExtensions
{
    public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<TData> resultTask)
    {
        var result = await resultTask;
        return result;
    }

    public static async Task<ActionResult> ToActionResult(this Task<Result> resultTask)
    {
        var result = await resultTask;

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(CreateProblemDetails(result.Errors));
        }

        return new OkResult();
    }

    public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<Result<TData>> resultTask)
    {
        var result = await resultTask;

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(CreateProblemDetails(result.Errors));
        }

        return result.Data;
    }

    public static async Task<ActionResult> ToActionResult(this Task<Stream> resultTask)
    {
        var result = await resultTask;

        return new FileStreamResult(result, MediaTypeNames.Image.Jpeg);
    }

    private static ProblemDetails CreateProblemDetails(IList<string> errors)
    {
        return new ProblemDetails
        {
            Title = "Workflow error",
            Detail = string.Join(", ", errors),
            Status = StatusCodes.Status400BadRequest,
            Extensions =
            {
                ["errorType"] = ProblemDetailErrorType.Application
            }
        };
    }
}