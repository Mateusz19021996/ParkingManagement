using ParkingManagement.src.domain.Entities;

namespace ParkingManagement.src.application.DTOs.Parking
{
    public class BookParkingDto
    {
        public string VehicleReg { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
