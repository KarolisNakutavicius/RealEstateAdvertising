using System.Security.Authentication;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace Application.Middlewares;

public class ContextMiddleware
{
    private readonly RequestDelegate _next;
    
    public ContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, IContextService contextService)
    {
        try
        {
            await contextService.GetCurrentUserAsync();
        }
        catch (AuthenticationException)
        {
            // user not logged in, just continue
        }

        await _next(context);
    }
}