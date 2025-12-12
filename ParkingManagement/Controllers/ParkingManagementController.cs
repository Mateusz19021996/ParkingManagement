using Microsoft.AspNetCore.Mvc;
using ParkingManagement.src.application.Abstractions;
using ParkingManagement.src.application.DTOs.Parking;

namespace ParkingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingManagementController : ControllerBase
    {        
        private readonly IParkingService _parkingService;
        private readonly ILogger<ParkingManagementController> _logger;

        public ParkingManagementController(IParkingService parkingService, ILogger<ParkingManagementController> logger)
        {
            _parkingService = parkingService;
            _logger = logger;
        }

        [HttpPost("/parking/exit")]
        public async Task<ActionResult<ExitParkingResultDto>> ExitParking([FromBody] ExitParkingDto exitParkingDto)
        {
            var exitParkingResult = await _parkingService.ExitParking(exitParkingDto);
            return Ok(exitParkingResult);
        }

        [HttpGet("/parking")]
        public async Task<ActionResult<ParkingCapacityInfoDto>> GetAvailableAndOccupiedSpaces()
        {
            var getParkingCapacityInfoResult = await _parkingService.GetParkingCapacityInfo();
            return Ok(getParkingCapacityInfoResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddParking([FromBody] BookParkingDto bookParkingDto)
        {
            var bookParkingResult = await _parkingService.BookParking(bookParkingDto);
            return Ok(bookParkingResult);
        }
    }
}
