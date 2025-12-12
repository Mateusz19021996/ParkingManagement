using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ParkingManagement.src.application.Abstractions;
using ParkingManagement.src.application.DTOs.Parking;
using ParkingManagement.src.application.DTOs.Vehicle;
using ParkingManagement.src.application.Services;
using ParkingManagement.src.domain.Abstractions;
using ParkingManagement.src.domain.Entities;


namespace Tests
{
    public class ParkingManagementTests
    {
        private readonly IParkingRepository _parkingRepository = Substitute.For<IParkingRepository>();
        private readonly IVehicleRepository _vehicleRepository = Substitute.For<IVehicleRepository>();
        private readonly IParkingManagementService _parkingManagementService = Substitute.For<IParkingManagementService>();

        private ParkingService CreateService() =>
            new ParkingService(_parkingRepository, _vehicleRepository, _parkingManagementService);


        [Fact]
        public async Task BookParking_ShouldThrow_WhenNoAvailableSpaces()
        {
            // Arrange
            _parkingRepository.GetFirstAvailableParkingSpace().ReturnsNull();

            var service = CreateService();

            var dto = new BookParkingDto
            {
                VehicleReg = "DW12345",
                VehicleType = VehicleType.SmallCar
            };

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.BookParking(dto));
        }

        [Fact]
        public async Task ExitParking_ShouldThrow_WhenVehicleNotFound()
        {
            // Arrange
            _parkingManagementService.GetVehicleByLicensePlate("ABC123").ReturnsNull();

            var service = CreateService();

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.ExitParking(new ExitParkingDto { VehicleReg = "ABC123" }));
        }

        [Fact]
        public async Task ExitParking_ShouldRemoveVehicle_AndReturnCorrectCharge()
        {
            // Arrange
            var entryTime = DateTime.Now.AddMinutes(-30);

            var vehicle = new ExitParkingVehicleDto
            {
                Id = 1,
                VehicleType = VehicleType.MediumCar,
                EntryTime = entryTime
            };

            _parkingManagementService.GetVehicleByLicensePlate("DWL1234")
                .Returns(vehicle);

            var service = CreateService();

            // Act
            var result = await service.ExitParking(new ExitParkingDto { VehicleReg = "DWL1234" });

            // Assert
            await _vehicleRepository.Received(1).RemoveVehicleById(1);

            // Assert for calculations
            Assert.Equal("DWL1234", result.VehicleReg);
            Assert.Equal(entryTime, result.TimeIn);

            Assert.True(result.VehicleCharge > 0);
        }
    }
}