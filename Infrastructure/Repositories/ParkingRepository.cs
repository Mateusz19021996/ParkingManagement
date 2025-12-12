using Microsoft.EntityFrameworkCore;
using ParkingManagement.src.domain.Abstractions;
using ParkingManagement.src.domain.Entities;

namespace ParkingManagement.src.infrastructure.Repositories
{
    public class ParkingRepository : IParkingRepository
    {

        private readonly AppDbContext _dbContext;

        public ParkingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ParkingSpace> GetFirstAvailableParkingSpace()
        {
            var availableSpace = await _dbContext.ParkingSpaces
                .Where(p => p.Vehicle == null)
                .FirstOrDefaultAsync();

            return availableSpace;
        }

        public async Task UpdateParkingSpace(ParkingSpace availableSpace)
        {
            _dbContext.ParkingSpaces.Update(availableSpace);
            await _dbContext.SaveChangesAsync();
        }
    }
}
