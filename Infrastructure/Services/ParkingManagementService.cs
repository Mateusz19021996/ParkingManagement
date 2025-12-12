using Microsoft.EntityFrameworkCore;
using ParkingManagement.src.application.Abstractions;
using ParkingManagement.src.application.DTOs.Parking;
using ParkingManagement.src.application.DTOs.Vehicle;

namespace ParkingManagement.src.infrastructure.Services
{
    public class ParkingManagementService : IParkingManagementService
    {
        private readonly AppDbContext _dbContext;

        public ParkingManagementService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ParkingCapacityInfoDto> GetParkingAvailibityInfo()
        {
            // There is a possibility to make it in one call by using GroupBy, but for the sake of simplicity and readability, I prefer to keep it this way.
            var available = await _dbContext.ParkingSpaces
                .CountAsync(p => p.Vehicle == null);

            var occupied = await _dbContext.ParkingSpaces
                .CountAsync(p => p.Vehicle != null);

            return new ParkingCapacityInfoDto
            {
                AvailableSpaces = available,
                OccupiedSpaces = occupied
            };
        }

        public async Task<ExitParkingVehicleDto> GetVehicleByLicensePlate(string vehicleReg)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.LicensePlate == vehicleReg)
                .Select(v => new ExitParkingVehicleDto
                {
                    Id = v.Id,
                    EntryTime = v.EntryTime,
                    VehicleType = v.VehicleType
                })
                .FirstOrDefaultAsync();

            return vehicle;  
        }
    }
}
