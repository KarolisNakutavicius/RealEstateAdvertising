using Domain.Entities;

namespace Application.Services.Contracts;

public interface IContextService
{
    Task<User> GetCurrentUserAsync();

    string GetUserId();
    
    bool IsAuthenticated { get; }
}