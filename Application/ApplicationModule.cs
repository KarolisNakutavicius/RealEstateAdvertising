using Application.Services;
using Application.Services.Contracts;
using Application.Services.QueryServices;
using Application.Services.QueryServices.QueryContracts;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAdvertisementService, AdvertisementService>();
        services.AddScoped<IAdvertisementQueryService, AdvertisementQueryService>();
        services.AddScoped<ICityQueryService, CityQueryService>();
        services.AddScoped<IFilterService, FilterService>();
        services.AddScoped<IContextService, ContextService>();

        return services;
    }
}