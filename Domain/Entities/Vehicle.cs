namespace ParkingManagement.src.domain.Entities
{
    public class Vehicle {         
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public VehicleType VehicleType { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
