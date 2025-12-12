namespace ParkingManagement.src.application.DTOs.Parking
{
    public class ExitParkingResultDto
    {
        public string VehicleReg { get; set; }
        public double VehicleCharge { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}
