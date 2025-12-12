using ParkingManagement.src.application.Abstractions;
using ParkingManagement.src.application.DTOs.Parking;
using ParkingManagement.src.domain.Abstractions;
using ParkingManagement.src.domain.Entities;

namespace ParkingManagement.src.application.Services
{
    public partial class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IParkingManagementService _parkingManagementService;

        public ParkingService(IParkingRepository parkingRepository, IVehicleRepository vehicleRepository, IParkingManagementService parkingManagementService)
        {
            _parkingRepository = parkingRepository;
            _vehicleRepository = vehicleRepository;
            _parkingManagementService = parkingManagementService;
        }

        public async Task<ExitParkingResultDto> ExitParking(ExitParkingDto exitParkingDto)
        {
            // if for some reasons this operation will take more then one minute, we want to treat user fairly and don't add operation time
            var timeOut = DateTime.Now;

            var exitVehicle = await _parkingManagementService.GetVehicleByLicensePlate(exitParkingDto.VehicleReg);

            if (exitVehicle == null)
            {
                throw new KeyNotFoundException("Vehicle not found in the parking lot.");
            }

            var mintesParked = (timeOut - exitVehicle.EntryTime).TotalMinutes;
            var totalCharge = GetPricePerInterval(exitVehicle.VehicleType, (int)mintesParked);

            await _vehicleRepository.RemoveVehicleById(exitVehicle.Id);

            return new ExitParkingResultDto
            {
                VehicleReg = exitParkingDto.VehicleReg,
                VehicleCharge = totalCharge,
                TimeIn = exitVehicle.EntryTime,
                TimeOut = timeOut
            };
        }

        public async Task<ParkingCapacityInfoDto> GetParkingCapacityInfo()
        {
            return await _parkingManagementService.GetParkingAvailibityInfo();            
        }

        public async Task<BookParkingResultDto> BookParking(BookParkingDto bookParkingDto)
        {
            var availableSpace = await _parkingRepository.GetFirstAvailableParkingSpace();

            if (availableSpace == null)
            {
                throw new InvalidOperationException("No available parking spaces.");
            }

            if (!Enum.IsDefined(typeof(VehicleType), bookParkingDto.VehicleType))
            {
                throw new ArgumentException("Inproper vehicle type");
            }

            var vehicle = new Vehicle
            {
                LicensePlate = bookParkingDto.VehicleReg,
                VehicleType = bookParkingDto.VehicleType,
                EntryTime = DateTime.Now
            };

            availableSpace.Vehicle = vehicle;
            await _parkingRepository.UpdateParkingSpace(availableSpace);

            return new BookParkingResultDto
            {
                VehicleReg = vehicle.LicensePlate,
                SpaceNumber = availableSpace.PlaceNumber,
                TimeIn = vehicle.EntryTime
            };
        }

        private double GetPricePerInterval(VehicleType vehicleType, double mintesParked)
        {
            var fiveMintesIntervals = Math.Floor(mintesParked / 5);
            var priceForEveryFiveMinutes = 1.00;

            switch (vehicleType)
            {
                case VehicleType.SmallCar:
                    return mintesParked * 0.1 + fiveMintesIntervals * priceForEveryFiveMinutes;
                case VehicleType.MediumCar:
                    return mintesParked * 0.2 + fiveMintesIntervals * priceForEveryFiveMinutes;
                case VehicleType.LargeCar:
                    return mintesParked * 0.4 + fiveMintesIntervals * priceForEveryFiveMinutes;
                default:
                    throw new ArgumentException($"Unknown vehicfle type: {vehicleType}");
            }
        }
    }
}
