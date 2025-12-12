using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.src.application.Abstractions;
using ParkingManagement.src.domain.Abstractions;
using ParkingManagement.src.infrastructure.Repositories;
using ParkingManagement.src.infrastructure.Services;
using ParkingManagement.src.Seeding;

namespace ParkingManagement.src.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<Seeder>();

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();
            services.AddScoped<IParkingManagementService, ParkingManagementService>();

            return services;
        }
    }
}
