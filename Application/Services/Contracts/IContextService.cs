using Domain.Entities;

namespace Application.Services.Contracts;

public interface IContextService
{
    Task<User> GetCurrentUserAsync();
    
    bool IsAuthenticated();
}