using Application.Services;
using Application.Services.Contracts;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<IContextService, ContextService>();

            return services;
        }
    }
}
