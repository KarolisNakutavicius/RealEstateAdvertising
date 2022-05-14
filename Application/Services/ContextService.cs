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
    private readonly HttpContext _context;
    
    public ContextService(UserManager<User> userManager, HttpContext contextService)
    {
        _userManager = userManager;
        _context = contextService;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        if (IsAuthenticated())
        {
            return _user;
        }
        
        var identity = _context.User.Identity;

        if (identity == null || !identity.IsAuthenticated)
        {
            throw new AuthenticationException(ErrorMessages.UserNotAuthenticated);
        }
        
        var userId = _context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new AuthenticationException(ErrorMessages.UserNotAuthenticated);
        }
        
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new AuthenticationException(ErrorMessages.UserNotAuthenticated);
        }

        _user = user;

        return user;
    }

    public bool IsAuthenticated() => _user != null;
}