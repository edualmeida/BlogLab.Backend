﻿using System.Net.Mime;
using Common.Application;
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
            return new BadRequestObjectResult(result.Errors);
        }

        return new OkResult();
    }

    public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<Result<TData>> resultTask)
    {
        var result = await resultTask;

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return result.Data;
    }

    public static async Task<ActionResult> ToActionResult(this Task<Stream> resultTask)
    {
        var result = await resultTask;

        return new FileStreamResult(result, MediaTypeNames.Image.Jpeg);
    }
}