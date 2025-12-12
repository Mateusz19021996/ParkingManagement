using Microsoft.EntityFrameworkCore;
using ParkingManagement.src.domain.Abstractions;

namespace ParkingManagement.src.infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _dbContext;

        public VehicleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RemoveVehicleById(int id)
        {
            // In real DB
            //await _dbContext.Vehicles
            //    .Where(v => v.Id == id)
            //    .ExecuteDeleteAsync();

            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle != null)
            {
                _dbContext.Vehicles.Remove(vehicle);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
