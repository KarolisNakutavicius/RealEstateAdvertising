using System.Security.Authentication;
using System.Security.Claims;
using Application.Resources;
using Application.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class ContextService : IContextService
{
    private User? _user;

    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public ContextService(UserManager<User> userManager, IHttpContextAccessor  contextAccessorService)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessorService;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        if (_user != null)
        {
            return _user;
        }

        var context = _contextAccessor.HttpContext;

        if (context == null)
        {
            throw new Exception("There is no handled http context attached");
        }
        
        var identity = context.User.Identity;

        if (identity == null || !identity.IsAuthenticated)
        {
            throw new AuthenticationException(ErrorMessages.UserNotAuthenticated);
        }
        
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new AuthenticationException(ErrorMessages.UserNotAuthenticated);
        }
        
        var user = await _userManager.FindByIdAsync(userId);

        _user = user ?? throw new AuthenticationException(ErrorMessages.UserNotAuthenticated);

        return user;
    }

    public bool IsAuthenticated => _user != null;
}