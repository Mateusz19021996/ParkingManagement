using ParkingManagement.src.application.DTOs.Parking;

namespace ParkingManagement.src.application.Abstractions
{
    public interface IParkingService
    {
        Task<BookParkingResultDto> BookParking(BookParkingDto bookParkingDto);
        Task<ExitParkingResultDto> ExitParking(ExitParkingDto exitParkingDto);
        Task<ParkingCapacityInfoDto> GetParkingCapacityInfo();
    }
}