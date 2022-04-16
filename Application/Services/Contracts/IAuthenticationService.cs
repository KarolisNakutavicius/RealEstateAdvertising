using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;

namespace Application.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<Result<AuthenticateResponse>> Login(AuthenticateRequest request);
        Task<Result<AuthenticateResponse>> Register(AuthenticateRequest request);
    }
}