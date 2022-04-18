using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AdvertismentService
    {
        private readonly IContextService _contextService;

        public AdvertismentService(IContextService contextService)
        {
            _contextService = contextService;
        }

        public async Task<Result<AdvertismentResponse>> CreateNewAdvertisment(CreateAdvertisementRequest request)
        {
            var user = await _contextService.GetCurrentUser();
            // retrieve owener ID using middleware

            var advertisment = Advertisement.CreateNew(user);

            return Result<AdvertismentResponse>.Ok(new AdvertismentResponse());
        }
    }
}
