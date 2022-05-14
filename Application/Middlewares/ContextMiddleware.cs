using System.Security.Authentication;
using Application.Services.Contracts;

namespace Application.Middlewares;

public class ContextMiddleware
{
    public async Task InvokeAsync(IContextService contextService)
    {
        try
        {
            await contextService.GetCurrentUserAsync();
        }
        catch (AuthenticationException)
        {
            // user not logged in, just continue
        }
    }
}