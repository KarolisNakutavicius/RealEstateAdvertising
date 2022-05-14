using Application.Services.Contracts;

namespace Application.Middlewares;

public class ContextMiddleware
{
    public async Task InvokeAsync(IContextService contextService)
    {
        await contextService.GetCurrentUserAsync();
    }
}