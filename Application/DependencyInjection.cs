using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.src.application.Abstractions;
using ParkingManagement.src.application.Services;

namespace ParkingManagement.src.application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IParkingService, ParkingService>();
            return services;
        }
    }
}
