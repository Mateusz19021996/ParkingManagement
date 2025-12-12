using ParkingManagement.src.domain.Entities;

namespace ParkingManagement.src.domain.Abstractions
{
    public interface IParkingRepository
    {
        Task<ParkingSpace> GetFirstAvailableParkingSpace();
        Task UpdateParkingSpace(ParkingSpace availableSpace);
    }
}