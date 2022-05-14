using Domain.Entities;

namespace Application.Services.Contracts;

public interface IContextService
{
    Task<User?> GetCurrentUserAsync();

    void SetUser(User user);
}