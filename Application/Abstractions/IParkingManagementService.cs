using ParkingManagement.src.application.DTOs.Parking;
using ParkingManagement.src.application.DTOs.Vehicle;

namespace ParkingManagement.src.application.Abstractions
{
    public interface IParkingManagementService
    {
        Task<ParkingCapacityInfoDto> GetParkingAvailibityInfo();
        Task<ExitParkingVehicleDto> GetVehicleByLicensePlate(string vehicleReg);
    }
}
