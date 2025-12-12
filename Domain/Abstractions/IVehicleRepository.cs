namespace ParkingManagement.src.domain.Abstractions
{
    public interface IVehicleRepository
    {
        Task RemoveVehicleById(int id);
    }
}
