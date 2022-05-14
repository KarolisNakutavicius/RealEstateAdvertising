using System.Security.Claims;
using Application.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Middlewares;

public class ContextMiddleware
{
    private readonly UserManager<User> _userManager;
    private readonly IContextService _contextService;
    
    public ContextMiddleware(UserManager<User> userManager, IContextService contextService)
    {
        _userManager = userManager;
        _contextService = contextService;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var identity = context.User.Identity;

        if (identity == null || !identity.IsAuthenticated)
        {
            return;
        }
        
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return;
        }
        
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return;
        }
        
        _contextService.SetUser(user);
    }
}