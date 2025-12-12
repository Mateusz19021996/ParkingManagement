using ParkingManagement.src.domain.Entities;

namespace ParkingManagement.src.application.DTOs.Vehicle
{
    public class ExitParkingVehicleDto
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
