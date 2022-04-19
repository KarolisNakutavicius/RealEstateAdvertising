using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;
using System.Security.Claims;

namespace Application.Services
{
    public class ContextService : IContextService
    {
        private readonly HttpContextAccessor _httpContextAccessor;

        private readonly UserManager<User> _userManager;

        public ContextService(HttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<User> GetCurrentUser()
        {
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext?.User.Identity!;

            if (!identity?.IsAuthenticated ?? false)
            {
                throw new AuthenticationException();
            }

            string userId = identity!.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AuthenticationException();

            return await _userManager.FindByIdAsync(userId);
        }
    }
}
