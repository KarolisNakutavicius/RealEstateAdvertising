using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Application.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToHttpResponse<T>(this Result<T> result)
    {
        if (!result.Success) return new BadRequestObjectResult(result.Errors);

        return new OkObjectResult(result.Data);
    }
    
    public static IActionResult ToHttpResponse(this Result result)
    {
        if (!result.Success) return new BadRequestObjectResult(result.Errors);

        return new OkResult();
    }
}