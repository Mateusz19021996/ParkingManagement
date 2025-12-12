namespace ParkingManagement.src.domain.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int PlaceNumber { get; set; }
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }        
    }
}
